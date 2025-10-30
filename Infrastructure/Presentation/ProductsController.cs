using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController (IServiceManager serviceManager):ControllerBase
    {
        //EndPoint:- Public non static method 
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
          var result=await serviceManager.ProductService.GetAllProductsAsync();
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
    }
}
