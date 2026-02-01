using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModules;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransfareObject.BasketModulsDTOS;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.ProductModules.Product;
namespace Service
{
    public class PaymentService(IConfiguration _configuration ,IBasketRepository _basketRepository
        ,IUnitOfWork _unitOfWork,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDTo> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            //Configare Stripe :Install package Stripe.net
            StripeConfiguration.ApiKey =_configuration["StripeSettings:SecretKey"];

            //Get Basket Using BasketId
            var Basket=await _basketRepository.GetBasketAsync(BasketId)?? throw new BasketNotFoundException(BasketId);

            //Get Amount -Get Product+Get DeliveryMethod 
            var ProductRepo = _unitOfWork.GetReository<Product, int>();

            foreach (var item in Basket.Items)
            {
                var Product =await  ProductRepo.GetByIdAsync(item.Id)?? throw new  ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DeliveryMethod=await _unitOfWork.GetReository<DeliveryMethod, int>()
                .GetByIdAsync(Basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);

            Basket.shippingPrice = DeliveryMethod.Cost;
                
            var BasketAmount=(long)(Basket.Items.Sum(item=>item.Quantity * item.Price)+DeliveryMethod.Cost)*100;

            //Create Payment Intent [Create or Update]
            var PaymentService = new PaymentIntentService();
            if(Basket.paymentIntentId is null)// Create
            {
                var Option = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent=await PaymentService.CreateAsync(Option);
                Basket.paymentIntentId=PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var Option = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount,
                };
                await PaymentService.UpdateAsync(Basket.paymentIntentId,Option);
            }
               await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            return _mapper.Map<BasketDTo>(Basket);
        }
    }
}
