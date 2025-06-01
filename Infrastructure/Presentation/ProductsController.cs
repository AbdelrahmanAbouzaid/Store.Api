using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
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
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<PaginationResponse<ProductResultDto>>(StatusCodes.Status200OK)]
        [Cache(100)]
        [Authorize]
        public async Task<IActionResult> GetAllProduct([FromQuery] ProductSpecificationParameters productSpecs)
        {
            var products = await serviceManager.ProductServices.GetAllProductsAsync(productSpecs);
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

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await serviceManager.ProductServices.GetAllBrandsAsync();
            if (brands is null) return BadRequest();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await serviceManager.ProductServices.GetAllTypesAsync();
            if (types is null) return BadRequest();
            return Ok(types);
        }

    }
}
