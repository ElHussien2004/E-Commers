using AutoMapper;
using DomainLayer.Models.OrderModules;
using Shared.DataTransfareObject.IdentityDTOS;
using Shared.DataTransfareObject.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToreturnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D=>D.buyerEmail ,O=>O.MapFrom(S=>S.BuyerEmail));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.product.ProductName))
                .ForMember(D=>D.PictureURL ,O=>O.MapFrom<OrderItemPictureURLResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

        }
    }
}
