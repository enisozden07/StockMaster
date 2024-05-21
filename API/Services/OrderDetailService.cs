using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class OrderDetailService
    {
        private readonly DataContext _context;

        public OrderDetailService(DataContext context)
        {
            _context = context;
        }

        public List<OrderDetail> Get()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail Get(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public void Create(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void Update(int id, OrderDetail orderDetail)
        {
            var existingOrderDetail = _context.OrderDetails.Find(id);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.OrderId = orderDetail.OrderId;
                existingOrderDetail.ProductId = orderDetail.ProductId;
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
            }
        }
    }
}
