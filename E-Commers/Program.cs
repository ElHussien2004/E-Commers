
using DomainLayer.Contracts;
using E_Commers.CustomMiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens.Experimental;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using System.Threading.Tasks;
using Shared.ErrorModels;
using Microsoft.AspNetCore.Http.HttpResults;
using E_Commers.Factories;
using E_Commers.Extensions;
namespace E_Commers
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region  Add services to the container.
            builder.Services.AddControllers();//Allow Depance Injection to API Service
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwagerService();
            builder.Services.AddCors(option => {
                option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            builder.Services.AddInfraStructureService(builder.Configuration);
          
            builder.Services.AddAplicationService();


            builder.Services.AddJWTService(builder.Configuration);
            builder .Services.AddAplicationServices();

            #endregion

            var app = builder.Build();
            #region DataSeeeding

             await app.SeedDataBaseAsync();

            #endregion

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
