using Shared.DataTransfareObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObject.OrderDTOS
{
    public class OrderToreturnDto
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } 
        public string DeliveryMethod { get; set; }

       

        public string status { get; set; }
        public AddressDTO shipToAddress { get; set; }
        public decimal deliveryCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];

        public decimal SubTotal { get; set; }
        

        public decimal Total { get; set; }
    }
}
