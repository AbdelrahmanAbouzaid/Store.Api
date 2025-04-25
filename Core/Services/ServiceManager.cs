using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        ICacheRepository cacheRepository,
        UserManager<AppUser> userManager,
        IMapper mapper) : IServiceManager
    {
        public IProductServices ProductServices { get; } = new ProductServices(unitOfWork, mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(basketRepository, mapper);

        public ICacheServices CacheServices { get; } = new CacheServices(cacheRepository);

        public IAuthServices AuthServices { get; } = new AuthServices(userManager);
    }
}
