using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class CustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public List<Customer> Get()
        {
            return _context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(int id, Customer customer)
        {
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.ContactInfo = customer.ContactInfo;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
