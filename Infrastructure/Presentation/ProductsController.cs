using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstraction;
using Shared;
using Shared.ErrorModels;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        //EndPoint:- Public non static method 
        [HttpGet]
        //we need sorting with 4 ways
        //1.nameasc
        //2.namedes
        //3.priceasc
        //4.pricedesc
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status200OK,Type =typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [Cache(100)]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery]ProductspecificationsParameters specparams)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(specparams);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        
        [HttpGet("{Id}")]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task <ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var result=await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result == null)return NotFound();
            return Ok(result);
        }

        [HttpGet("brands")]
       [ ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var result=await serviceManager.ProductService.GetAllBrandsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("types")]

        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var result=await serviceManager.ProductService.GetAllTypesAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
