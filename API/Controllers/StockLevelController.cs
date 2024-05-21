using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

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
        public ActionResult<List<StockLevel>> Get() => _stockLevelService.Get();

        [HttpGet("{id}")]
        public ActionResult<StockLevel> Get(int id)
        {
            var stockLevel = _stockLevelService.Get(id);
            if (stockLevel == null)
            {
                return NotFound();
            }
            return stockLevel;
        }

        [HttpPost]
        public ActionResult<StockLevel> Create(StockLevel stockLevel)
        {
            _stockLevelService.Create(stockLevel);
            return CreatedAtAction(nameof(Get), new { id = stockLevel.Id }, stockLevel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StockLevel stockLevel)
        {
            var existingStockLevel = _stockLevelService.Get(id);
            if (existingStockLevel == null)
            {
                return NotFound();
            }
            _stockLevelService.Update(id, stockLevel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stockLevel = _stockLevelService.Get(id);
            if (stockLevel == null)
            {
                return NotFound();
            }
            _stockLevelService.Delete(id);
            return NoContent();
        }
    }
}
