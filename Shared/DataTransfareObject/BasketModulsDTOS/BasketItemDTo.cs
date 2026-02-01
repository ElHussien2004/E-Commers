using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObject.BasketModulsDTOS
{
    public class BasketItemDTo
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = default!;

        public string PictureURL { get; set; } = default!;
        [Range (1,double.MaxValue)]
        public decimal Price { get; set; }

        [Range (1,100)]
        public int Quantity { get; set; }

        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
