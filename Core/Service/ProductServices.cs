using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModules;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransfareObject.ProductModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductServices(IUnitOfWork _unitOfWork ,IMapper _mapper ) : IProductService
    {
        public  async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetReository<ProductBrand, int>();
            var Brands =   await Repo.GetAllAsync();
            var BrandDto = _mapper.Map<IEnumerable<ProductBrand> , IEnumerable<BrandDTO>>(Brands);
             return BrandDto;
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQuery productQuery)
        {
            var Repo = _unitOfWork.GetReository<Product, int>();
            var Products =await Repo.GetAllAsync(new ProductWithBrandAndTypeSpecification(productQuery));
          
            var Data= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);

            var count = await Repo.CountAsync(new ProductCountSpecification(productQuery));
            return new PaginatedResult<ProductDTO>(productQuery.pageNumber, Data.Count(), count, Data);
           
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Repo = _unitOfWork.GetReository<ProductType, int>();
            var Types = await Repo.GetAllAsync();
            var typeDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
            return typeDto;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var Repo =  _unitOfWork.GetReository<Product, int>();
            var Specification = new ProductWithBrandAndTypeSpecification(id);
            var Product =await Repo.GetByIdAsync(Specification);
           if (Product is null)
           {
                throw new ProductNotFoundException(id);
           }
            return _mapper.Map<Product, ProductDTO>(Product);

        }
    }
}
