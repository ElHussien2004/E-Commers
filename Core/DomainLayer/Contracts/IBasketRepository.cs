using DomainLayer.Models.BasketModuls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket ,TimeSpan? TimeToLive=null);

        Task<bool> DelateBasketAsync(string id);
    }
}
