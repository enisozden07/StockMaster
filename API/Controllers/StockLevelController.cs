using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockLevelsController : ControllerBase
    {
        private readonly StockLevelService _stockLevelService;

        public StockLevelsController(StockLevelService stockLevelService)
        {
            _stockLevelService = stockLevelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockLevel>>> GetStockLevels()
        {
            var stockLevels = await _stockLevelService.GetStockLevels();
            return Ok(stockLevels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockLevel>> GetStockLevel(int id)
        {
            var stockLevel = await _stockLevelService.GetStockLevelById(id);
            if (stockLevel == null)
            {
                return NotFound();
            }
            return Ok(stockLevel);
        }

        [HttpPost]
        public async Task<ActionResult<StockLevel>> AddStockLevel(StockLevel stockLevel)
        {
            var createdStockLevel = await _stockLevelService.AddStockLevel(stockLevel);
            return CreatedAtAction(nameof(GetStockLevel), new { id = createdStockLevel.Id }, createdStockLevel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockLevel(int id, StockLevel stockLevel)
        {
            if (id != stockLevel.Id)
            {
                return BadRequest();
            }
            await _stockLevelService.UpdateStockLevel(stockLevel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockLevel(int id)
        {
            await _stockLevelService.DeleteStockLevel(id);
            return NoContent();
        }
    }
}
