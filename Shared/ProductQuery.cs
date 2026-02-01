using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQuery
    {
        private const int DefueltPageSize = 5;
        private const int MaxPageSize = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSorting sort { get; set; }

        public string? search {  get; set; }

        private int pagesize=DefueltPageSize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value >MaxPageSize ? MaxPageSize: value; }
        }



        public int pageNumber { get; set; } = 1;


    }
}
