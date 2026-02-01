using DomainLayer.Models.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class OrderWithPaymentIntentSpecifications:BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentSpecifications(string PaymentIntendId):base(o=>o.PaymentIntentId==PaymentIntendId)
        {
            
        }
    }
}
