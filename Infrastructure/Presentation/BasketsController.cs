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
    public class BasketsController(IServiceManager serviceManager):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasket(string id)
        {
            var result=await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>UpdateGetBasket(BasketDto basket)
        {
            var result=await serviceManager.BasketService.UpdateBasketAsync(basket);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult>DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
