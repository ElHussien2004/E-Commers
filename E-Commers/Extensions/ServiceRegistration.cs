using E_Commers.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commers.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection  AddSwagerService (this IServiceCollection Services)
        {

            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
           Services.Configure<ApiBehaviorOptions>(Option =>
            {
                Option.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrors;

            });
            return Services;
        }

        public static IServiceCollection AddJWTService (this IServiceCollection Services ,IConfiguration configuration)
        {

            Services.AddAuthentication(Conf =>
            {
                Conf.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                Conf.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Option=>
            {
                Option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"]))
                };
            }) ;
            
            
            
            return Services;
        }
    }
}
