using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class OrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public List<Order> Get()
        {
            return _context.Orders.ToList();
        }

        public Order Get(int id)
        {
            return _context.Orders.Find(id);
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(int id, Order order)
        {
            var existingOrder = _context.Orders.Find(id);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.TotalAmount = order.TotalAmount;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
