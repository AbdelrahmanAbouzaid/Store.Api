using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketServices(IBasketRepository basketRepository, IMapper mapper) : IBasketServices
    {
        public async Task<BasketDto?> GetBaskesAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket == null) throw new BasketNotFoundException(id);
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<BasketDto?> UpdateBaskesAsync(BasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);
            basket = await basketRepository.UpdateBasketAsync(basket);
            if (basket == null) throw new BasketCreateOrUpdateExeption();

            var result = mapper.Map<BasketDto>(basket);
            return result;

        }

        public async Task<bool> DeleteBaskesAsync(string id)
        {
            var flag = await basketRepository.DeleteBasketAsync(id);
            return flag ? flag : throw new BasketDeleteException();
        }

       
    }
}
