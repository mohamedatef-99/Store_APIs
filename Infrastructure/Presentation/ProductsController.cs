using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // endpoint: public non-static method
        [HttpGet] // Get: /api/products
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await serviceManager.ProductService.GetAllProductAsync();
            if (result is null) return BadRequest();

            return Ok(result); // 200
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result); // 200
        }
    }
}

