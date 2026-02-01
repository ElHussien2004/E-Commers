using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModuls;
using ServiceAbstraction;
using Shared.DataTransfareObject.BasketModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository ,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTo> CreateOrUpdateAsync(BasketDTo basket)
        {
            var CustomBasket =_mapper.Map<BasketDTo,CustomerBasket>(basket);
            var IsDone =await _basketRepository.CreateOrUpdateBasketAsync(CustomBasket);
            if (IsDone is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can Not Create Or Update Basket Now ,Try Again Later..");
        }

        public async Task<bool> DeleteBasketAsync(string Id)=>await _basketRepository.DelateBasketAsync(Id);

        public async Task<BasketDTo> GetBasketAsync(string Key)
        {
           var Basket =await _basketRepository.GetBasketAsync(Key);

            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDTo>(Basket);
            else 
                throw new BasketNotFoundException(Key);
        }
    }
}
