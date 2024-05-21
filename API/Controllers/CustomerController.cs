using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customerService.Get();

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            _customerService.Create(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer customer)
        {
            var existingCustomer = _customerService.Get(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            _customerService.Update(id, customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            _customerService.Delete(id);
            return NoContent();
        }
    }
}
