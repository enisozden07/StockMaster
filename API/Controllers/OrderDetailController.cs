using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailService _orderDetailService;

        public OrderDetailsController(OrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public ActionResult<List<OrderDetail>> Get() => _orderDetailService.Get();

        [HttpGet("{id}")]
        public ActionResult<OrderDetail> Get(int id)
        {
            var orderDetail = _orderDetailService.Get(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return orderDetail;
        }

        [HttpPost]
        public ActionResult<OrderDetail> Create(OrderDetail orderDetail)
        {
            _orderDetailService.Create(orderDetail);
            return CreatedAtAction(nameof(Get), new { id = orderDetail.Id }, orderDetail);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderDetail orderDetail)
        {
            var existingOrderDetail = _orderDetailService.Get(id);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }
            _orderDetailService.Update(id, orderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderDetail = _orderDetailService.Get(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            _orderDetailService.Delete(id);
            return NoContent();
        }
    }
}
