using DomainLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class AplicationServiceRegistration
    {
        public static IServiceCollection  AddAplicationService (this IServiceCollection Services)
        {
            Services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));
            Services.AddAutoMapper(x=>x.AddProfile(new BasketProfile()));
            Services.AddAutoMapper(x => x.AddProfile(new IdentityProfile()));
            Services.AddAutoMapper(x => x.AddProfile(new OrderProfile()));
            //builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            Services.AddTransient<Service.MappingProfiles.PictureURLResolver>();
            Services.AddTransient<Service.MappingProfiles.OrderItemPictureURLResolver>();


            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();


            Services.AddScoped<IProductService, ProductServices>();
            Services.AddScoped<Func<IProductService>>(Profider=>
                ()=> Profider.GetRequiredService<IProductService>());

            Services.AddScoped<IOrderService,OrderService>();
            Services.AddScoped<Func<IOrderService>>(Profider =>
                () => Profider.GetRequiredService<IOrderService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(Profider =>
                () => Profider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(Profider =>
                () => Profider.GetRequiredService<IBasketService>());

            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(Profider =>
                () => Profider.GetRequiredService<IPaymentService>());

            // Services.AddScoped<ICacheRepository, CacheRepository>();
            Services.AddScoped<ICacheService, CacheService>();
            return Services;
        }
    }
}
