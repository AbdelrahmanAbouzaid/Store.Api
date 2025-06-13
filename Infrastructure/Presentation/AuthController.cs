using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
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
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userResult = await serviceManager.AuthServices.LoginAsync(loginDto);
            return Ok(userResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var userResult = await serviceManager.AuthServices.RegisterAsync(registerDto);
            return Ok(userResult);
        }

        [HttpGet("EmailExists")]
        public async Task<IActionResult> CkeckEmailExist(string email)
        {
            var result = await serviceManager.AuthServices.CheckEmailExistAsync(email);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthServices.GetCurrentUserAsync(email);
            return Ok(result);
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthServices.GetCurrentUserAddressAsync(email);
            return Ok(result);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthServices.UpdateCurrentUserAddressAsync(addressDto, email);
            return Ok(result);
        }
    }
}
