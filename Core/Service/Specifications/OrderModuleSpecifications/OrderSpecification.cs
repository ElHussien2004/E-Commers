using DomainLayer.Models.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.OrderModuleSpecifications
{
    internal class OrderSpecification:BaseSpecifications<Order,Guid>
    {
        //Get All Order
        public OrderSpecification(string Email):base(E=>E.BuyerEmail==Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
        public OrderSpecification(Guid id) : base(E => E.Id ==id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            
        }
    }
}
