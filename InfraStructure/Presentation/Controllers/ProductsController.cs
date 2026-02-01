using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransfareObject.ProductModulsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    
    public class ProductsController(IServiceManager _serviceManager) :ApiBaseController
    {
        //GetAllProduct
        [HttpGet]
        [Cache]
        //[Authorize]
        public async Task<ActionResult <PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQuery productQuery)
        {
            var Products =await  _serviceManager.ProductService.GetAllProductsAsync(productQuery);
            return Ok(Products);
        }

        //GetProduct By ID
        [HttpGet ("{id:int}")]
       
        public async Task<ActionResult<ProductDTO>> GetProductById(int id) 
        {
            var Product =await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);

        }
        [HttpGet ("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>>GetTypes()
        {
            var Types =await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
