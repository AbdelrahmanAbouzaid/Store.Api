using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var basket = await serviceManager.BasketServices.GetBaskesAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await serviceManager.BasketServices.UpdateBaskesAsync(basketDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            await serviceManager.BasketServices.DeleteBaskesAsync(id);
            return NoContent();
        }
    }
}
