using Shared.DataTransfareObject.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        //Create Order 

        Task<OrderToreturnDto> CreateOrderAsync(OrderDTo orderDTo, string Email);

        //Get Delivery Methods 

         Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();

        //Get aLL orders

        Task<IEnumerable<OrderToreturnDto>> GetAllOrdersAsync( string Email);

        //Get Order By Id

        Task<OrderToreturnDto> GetOrderByIdAsync(Guid Id);

    }
}
