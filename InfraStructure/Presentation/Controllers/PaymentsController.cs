using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObject.BasketModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager):ApiBaseController
    {
        //Create Or Update Payment Intent 
        //comment
        [Authorize]
        [HttpPost ("{Basketid}")]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePaymentIntent(string Basketid)
        {
            var Basket=await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(Basketid);
            return Ok(Basket);
        }
    }
}
