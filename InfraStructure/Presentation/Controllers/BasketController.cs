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

    public class BasketsController(IServiceManager _serviceManager) :ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTo>> GetBasketAsync(string Key)
        {
            var basket=await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDTo >> CreateOrUpdateBasketAsync(BasketDTo basket)
        {
            var Basket =await _serviceManager.BasketService.CreateOrUpdateAsync(basket);
            return Ok(Basket);
        }

        [HttpDelete("{Key}")]

        public async Task<ActionResult<bool>> DeleteBasketAsync(string Key)
        {
            var IsDeleted=await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(IsDeleted);
        }
    }
}
