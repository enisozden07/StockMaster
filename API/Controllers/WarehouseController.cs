using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;

        public WarehousesController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public ActionResult<List<Warehouse>> Get() => _warehouseService.Get();

        [HttpGet("{id}")]
        public ActionResult<Warehouse> Get(int id)
        {
            var warehouse = _warehouseService.Get(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return warehouse;
        }

        [HttpPost]
        public ActionResult<Warehouse> Create(Warehouse warehouse)
        {
            _warehouseService.Create(warehouse);
            return CreatedAtAction(nameof(Get), new { id = warehouse.Id }, warehouse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Warehouse warehouse)
        {
            var existingWarehouse = _warehouseService.Get(id);
            if (existingWarehouse == null)
            {
                return NotFound();
            }
            _warehouseService.Update(id, warehouse);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var warehouse = _warehouseService.Get(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            _warehouseService.Delete(id);
            return NoContent();
        }
    }
}
