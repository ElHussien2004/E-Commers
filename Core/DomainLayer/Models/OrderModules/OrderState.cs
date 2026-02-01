using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModules
{
   public enum OrderState
    {
        Pending = 0,
        PaymentRecevied = 1,
        PaymentFailed = 2,
    }
}
