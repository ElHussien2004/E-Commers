using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork ,IMapper mapper ,IBasketRepository basketRepository ,UserManager<ApplicationUser> userManager ,IConfiguration configuration) 
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductServices(unitOfWork, mapper));
        public IProductService ProductService =>_LazyproductService.Value;



        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _LazyBasketService.Value;



        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration,mapper));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(()=>new OrderService(mapper, basketRepository,unitOfWork));

        public IOrderService OrderService => _LazyOrderService.Value;
    }
}
