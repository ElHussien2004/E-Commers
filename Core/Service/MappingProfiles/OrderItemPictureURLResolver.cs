using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModules;
using Microsoft.Extensions.Configuration;
using Shared.DataTransfareObject.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderItemPictureURLResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {

            if (string.IsNullOrEmpty(source.product.PictureURL))
            {
                return string.Empty;
            }
            else
            {
                var picture = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.product.PictureURL}";
                return picture;
            }
        }
    }
}
