using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SuppliersController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public ActionResult<List<Supplier>> Get() => _supplierService.Get();

        [HttpGet("{id}")]
        public ActionResult<Supplier> Get(int id)
        {
            var supplier = _supplierService.Get(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return supplier;
        }

        [HttpPost]
        public ActionResult<Supplier> Create(Supplier supplier)
        {
            _supplierService.Create(supplier);
            return CreatedAtAction(nameof(Get), new { id = supplier.Id }, supplier);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Supplier supplier)
        {
            var existingSupplier = _supplierService.Get(id);
            if (existingSupplier == null)
            {
                return NotFound();
            }
            _supplierService.Update(id, supplier);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var supplier = _supplierService.Get(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _supplierService.Delete(id);
            return NoContent();
        }
    }
}
