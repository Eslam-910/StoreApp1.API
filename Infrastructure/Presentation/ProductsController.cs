using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

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
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductspecificationsParameters specparams)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(specparams);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpGet("{Id}")]
        public async Task <IActionResult> GetProductById(int id)
        {
            var result=await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result == null)return NotFound();
            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result=await serviceManager.ProductService.GetAllBrandsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result=await serviceManager.ProductService.GetAllTypesAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
