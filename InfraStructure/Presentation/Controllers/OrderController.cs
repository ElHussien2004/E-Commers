using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ServiceAbstraction;
using Shared.DataTransfareObject.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
        ///Create Order 
     
        [HttpPost]
        public async Task<ActionResult<OrderToreturnDto>> CreateOrder(OrderDTo orderDTo)
        {
            var Order=await _serviceManager.OrderService.CreateOrderAsync(orderDTo,GetEmailFromToken());
            return Ok(Order);
        }

        //Get Delivery Methods 
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]

        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMothod()
        {
            var Del =await  _serviceManager.OrderService.GetDeliveryMethodAsync();
            return Ok(Del);
        }

        //GetAll Orders
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToreturnDto>>> GetAllOrder()
        {
            var Orders=await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        //Get Order By Id
     
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<OrderToreturnDto>>GetOrderById(Guid id)
        {
            var Order=await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
