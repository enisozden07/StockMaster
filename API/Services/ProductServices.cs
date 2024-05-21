using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public List<Product> Get()
        {
            return _context.Products.ToList();
        }

        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(int id, Product product)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.SupplierId = product.SupplierId;
                existingProduct.Price = product.Price;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
