using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class WarehouseService
    {
        private readonly DataContext _context;

        public WarehouseService(DataContext context)
        {
            _context = context;
        }

        public List<Warehouse> Get()
        {
            return _context.Warehouses.ToList();
        }

        public Warehouse Get(int id)
        {
            return _context.Warehouses.Find(id);
        }

        public void Create(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();
        }

        public void Update(int id, Warehouse warehouse)
        {
            var existingWarehouse = _context.Warehouses.Find(id);
            if (existingWarehouse != null)
            {
                existingWarehouse.WarehouseName = warehouse.WarehouseName;
                existingWarehouse.Location = warehouse.Location;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var warehouse = _context.Warehouses.Find(id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
                _context.SaveChanges();
            }
        }
    }
}
