using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModules;
using DomainLayer.Models.ProductModules;
using Service.Specifications;
using Service.Specifications.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransfareObject.IdentityDTOS;
using Shared.DataTransfareObject.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService (IMapper _mapper ,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork): IOrderService
    {
        public async Task<OrderToreturnDto> CreateOrderAsync(OrderDTo orderDTo, string Email)
        {
             //Map AddressDto To Address Order
             var OrderAddress=_mapper.Map<AddressDTO,OrderAddress>(orderDTo.shipToAddress);
            //Get Basket

            var Basket = await _basketRepository.GetBasketAsync(orderDTo.BasketId)
                ?? throw new BasketNotFoundException(orderDTo.BasketId);

            ArgumentNullException.ThrowIfNullOrEmpty(Basket.paymentIntentId);

            var OrderRepo = _unitOfWork.GetReository<Order, Guid>();

            var OrderSpec = new OrderWithPaymentIntentSpecifications(Basket.paymentIntentId);

            var ExistOrder =await OrderRepo.GetByIdAsync(OrderSpec);
             if(ExistOrder is not null)  OrderRepo.Remove(ExistOrder);


            //Create OrderItems List

            List<OrderItem> orderItems = [];

            var Rep = _unitOfWork.GetReository<Product, int>();

            foreach (var item in Basket.Items)
            {
                var ProductFromDatabase = await Rep.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);


                orderItems.Add(CreateOrderItem(item, ProductFromDatabase));
            }
            //Get Delivery Method 
            var Delivery=await _unitOfWork.GetReository<DeliveryMethod,int>().GetByIdAsync(orderDTo.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDTo.DeliveryMethodId);

            //Calculate SubTotal
            var SubTotal = orderItems.Sum(O => O.Quantity * O.Price);

            var Order=new Order(Email,OrderAddress,Delivery,orderItems, SubTotal ,Basket.paymentIntentId);

            await OrderRepo.AddAsync(Order);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<Order,OrderToreturnDto>(Order);

        }

        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModuls.BasketItem item, Product ProductFromDatabase)
        {
            return new OrderItem()
            {
                product = new ProductItemOrdered()
                {
                    ProductId = ProductFromDatabase.Id,
                    PictureURL = ProductFromDatabase.PictureUrl
                                    ,
                    ProductName = ProductFromDatabase.Name
                },

                Price = ProductFromDatabase.Price,
                Quantity = item.Quantity,
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var Del=await _unitOfWork.GetReository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>,IEnumerable <DeliveryMethodDto>>(Del);
        }

        public async Task<IEnumerable<OrderToreturnDto>> GetAllOrdersAsync(string Email)
        {
            var spec = new OrderSpecification(Email);
            var Orders =await  _unitOfWork.GetReository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>,IEnumerable <OrderToreturnDto>>(Orders);

        }

        public async Task<OrderToreturnDto> GetOrderByIdAsync(Guid Id)
        {
            var spec = new OrderSpecification(Id);
            var Order = await _unitOfWork.GetReository<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order, OrderToreturnDto>(Order);
        }
    }
}
