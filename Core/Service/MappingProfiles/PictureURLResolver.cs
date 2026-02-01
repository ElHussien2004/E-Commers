using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DomainLayer.Models.ProductModules;
using Shared.DataTransfareObject.ProductModulsDTOS;
namespace Service.MappingProfiles
{
    public class PictureURLResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty; 
            }
            else
            {
                var picture = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return picture;
            }
        }
    }
}
