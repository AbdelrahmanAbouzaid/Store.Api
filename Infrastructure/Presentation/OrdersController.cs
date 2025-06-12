using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDto orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderServices.CreateOrderAsync(orderRequest, email);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.OrderServices.GetOrdersByUserEmailAsync(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await serviceManager.OrderServices.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            var deliveryMethods = await serviceManager.OrderServices.GetAllDeliveryMethods();
            return Ok(deliveryMethods);
        }
    }
}
