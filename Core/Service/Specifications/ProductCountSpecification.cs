using DomainLayer.Models.ProductModules;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductCountSpecification:BaseSpecifications<Product ,int>
    {
        public ProductCountSpecification(ProductQuery productQuery): base(P=>(!productQuery.BrandId.HasValue ||P.BrandId== productQuery.BrandId) && (!productQuery.TypeId.HasValue || P.TypeId == productQuery.TypeId) 
            &&((string.IsNullOrWhiteSpace(productQuery.search)) || (P.Name.ToLower().Contains(productQuery.search.ToLower()))))
        {
            
        }
    }
}
