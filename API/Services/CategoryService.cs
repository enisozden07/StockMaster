using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class CategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public List<Category> Get()
        {
            return _context.Categories.ToList();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(int id, Category category)
        {
            var existingCategory = _context.Categories.Find(id);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
