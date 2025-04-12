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
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await serviceManager.ProductServices.GetAllProductsAsync();
            if (products is null) return BadRequest();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await serviceManager.ProductServices.GetProductByIdAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await serviceManager.ProductServices.GetAllBrandsAsync();
            if(brands is null) return BadRequest();
            return Ok(brands);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await serviceManager.ProductServices.GetAllTypesAsync();
            if (types is null) return BadRequest();
            return Ok(types);
        }

    }
}
