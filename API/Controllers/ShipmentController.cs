using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly ShipmentService _shipmentService;

        public ShipmentsController(ShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet]
        public ActionResult<List<Shipment>> Get() => _shipmentService.Get();

        [HttpGet("{id}")]
        public ActionResult<Shipment> Get(int id)
        {
            var shipment = _shipmentService.Get(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return shipment;
        }

        [HttpPost]
        public ActionResult<Shipment> Create(Shipment shipment)
        {
            _shipmentService.Create(shipment);
            return CreatedAtAction(nameof(Get), new { id = shipment.Id }, shipment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Shipment shipment)
        {
            var existingShipment = _shipmentService.Get(id);
            if (existingShipment == null)
            {
                return NotFound();
            }
            _shipmentService.Update(id, shipment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var shipment = _shipmentService.Get(id);
            if (shipment == null)
            {
                return NotFound();
            }
            _shipmentService.Delete(id);
            return NoContent();
        }
    }
}
