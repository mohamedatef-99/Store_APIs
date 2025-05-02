﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketId(string id)
        {
            var result = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
