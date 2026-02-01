using DomainLayer.Contracts;
using E_Commers.CustomMiddleWares;
using Microsoft.AspNetCore.Builder;
namespace E_Commers.Extensions
{
    public static class WebAplicationRegistrations
    {
        public static async Task SeedDataBaseAsync (this WebApplication app)
        {
            using var Scoop = app.Services.CreateScope();

            var ObjectDataSeeding = Scoop.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectDataSeeding.DataSeedingAsync();
            await ObjectDataSeeding.IdentityDataSeedingAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
