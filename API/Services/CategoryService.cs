using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class CategoryService
    {
        private readonly string? _connectionString;

        public CategoryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Categories";
                return await connection.QueryAsync<Category>(query);
            }
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Categories WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Category>(query, new { Id = id });
            }
        }

        public async Task<Category> AddCategory(Category category)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
                await connection.ExecuteAsync(query, category);
                return category;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE Id = @Id";
                await connection.ExecuteAsync(query, category);
            }
        }

        public async Task DeleteCategory(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Categories WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
