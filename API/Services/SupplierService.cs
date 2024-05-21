using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class SupplierService
    {
        private readonly DataContext _context;

        public SupplierService(DataContext context)
        {
            _context = context;
        }

        public List<Supplier> Get()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier Get(int id)
        {
            return _context.Suppliers.Find(id);
        }

        public void Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void Update(int id, Supplier supplier)
        {
            var existingSupplier = _context.Suppliers.Find(id);
            if (existingSupplier != null)
            {
                existingSupplier.SupplierName = supplier.SupplierName;
                existingSupplier.ContactInfo = supplier.ContactInfo;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
        }
    }
}
