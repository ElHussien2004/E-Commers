using Shared;
using Shared.DataTransfareObject.ProductModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //GetAll Products
        Task<PaginatedResult<ProductDTO>>GetAllProductsAsync(ProductQuery productQuery);
        //GetProductByID
        Task<ProductDTO> GetProductByIdAsync (int id);

        //GetALLBrands
        Task<IEnumerable<BrandDTO>>GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>>GetAllTypesAsync ();
    }
}
