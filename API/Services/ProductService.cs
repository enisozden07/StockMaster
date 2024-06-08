using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class ProductService
    {
        private readonly string? _connectionString;

        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Products";
                return await connection.QueryAsync<Product>(query);
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Products WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Products (Name, Description, Price, CategoryId) VALUES (@Name, @Description, @Price, @CategoryId)";
                await connection.ExecuteAsync(query, product);
                return product;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, CategoryId = @CategoryId WHERE Id = @Id";
                await connection.ExecuteAsync(query, product);
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Products WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
