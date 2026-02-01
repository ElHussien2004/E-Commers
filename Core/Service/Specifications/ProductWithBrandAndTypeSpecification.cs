using DomainLayer.Models.ProductModules;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQuery productQuery)
            : base(P=>(!productQuery.BrandId.HasValue ||P.BrandId== productQuery.BrandId) && (!productQuery.TypeId.HasValue || P.TypeId == productQuery.TypeId) 
            &&((string.IsNullOrWhiteSpace(productQuery.search)) || (P.Name.ToLower().Contains(productQuery.search.ToLower()))))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (productQuery.sort)
            {
                case ProductSorting.NameDesc:
                    AddOrderByDescending(P=>P.Name);
                    break;
                case ProductSorting.NameAsc:
                    AddOrderBy(P=>P.Name);
                    break;
                case ProductSorting.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                case ProductSorting.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                default:
                    break;


            }

            AddPagination(productQuery.PageSize, productQuery.pageNumber);
        }

        public ProductWithBrandAndTypeSpecification( int id):base(P=>P.Id==id) 
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
