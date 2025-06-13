using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await serviceManager.PaymentServices.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(basket);
        }
    }
}
