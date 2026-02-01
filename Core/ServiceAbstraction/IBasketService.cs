using Shared.DataTransfareObject.BasketModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDTo> GetBasketAsync(string Key);

        Task<BasketDTo> CreateOrUpdateAsync(BasketDTo basket);

        Task<bool> DeleteBasketAsync(string Id);
    }
}
