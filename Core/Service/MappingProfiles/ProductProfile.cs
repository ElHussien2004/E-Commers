using AutoMapper;
using DomainLayer.Models.ProductModules;
using Shared.DataTransfareObject.ProductModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
   public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
              .ForMember(des=>des.productBrand ,OP=>OP.MapFrom(sr=>sr.ProductBrand.Name)) 
              .ForMember(des => des.productType, OP => OP.MapFrom(sr => sr.ProductType.Name))
              .ForMember(des=>des.PictureUrl ,op=>op.MapFrom<PictureURLResolver>());
            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
        }
    }
}
