using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
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
        IOptions<JwtOptions> options,
        IMapper mapper,
        IConfiguration configuration
        ) : IServiceManager
    {
        public IProductServices ProductServices { get; } = new ProductServices(unitOfWork, mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(basketRepository, mapper);

        public ICacheServices CacheServices { get; } = new CacheServices(cacheRepository);

        public IAuthServices AuthServices { get; } = new AuthServices(userManager, options, mapper);

        public IOrderServices OrderServices { get; } = new OrderServices(mapper, basketRepository, unitOfWork);

        public IPaymentServices PaymentServices { get; } = new PaymentServices(basketRepository, unitOfWork, mapper, configuration);
    }
}
