using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() => _orderService.Get();

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _orderService.Create(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Order order)
        {
            var existingOrder = _orderService.Get(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            _orderService.Update(id, order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            _orderService.Delete(id);
            return NoContent();
        }
    }
}
