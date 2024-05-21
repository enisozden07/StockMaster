using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class StockLevelService
    {
        private readonly DataContext _context;

        public StockLevelService(DataContext context)
        {
            _context = context;
        }

        public List<StockLevel> Get()
        {
            return _context.StockLevels.ToList();
        }

        public StockLevel Get(int id)
        {
            return _context.StockLevels.Find(id);
        }

        public void Create(StockLevel stockLevel)
        {
            _context.StockLevels.Add(stockLevel);
            _context.SaveChanges();
        }

        public void Update(int id, StockLevel stockLevel)
        {
            var existingStockLevel = _context.StockLevels.Find(id);
            if (existingStockLevel != null)
            {
                existingStockLevel.ProductId = stockLevel.ProductId;
                existingStockLevel.WarehouseId = stockLevel.WarehouseId;
                existingStockLevel.Quantity = stockLevel.Quantity;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var stockLevel = _context.StockLevels.Find(id);
            if (stockLevel != null)
            {
                _context.StockLevels.Remove(stockLevel);
                _context.SaveChanges();
            }
        }
    }
}
