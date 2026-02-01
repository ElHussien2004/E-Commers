using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModules
{
    public class Order:BaseEntity<Guid>
    {

        public Order()
        {
            
        }
        public Order(string userAddress, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = userAddress;
            shipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }


        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        
        public int DeliveryMethodId { get; set; }

        public OrderState status { get; set; }
       
       


        /*
         * [NotMapped]
         * public decimal Total { get => SubTotal + DeliveryMethod.Price; }
        */
        
        public decimal GetTotal ()=>SubTotal+ DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }
        
    }
}
