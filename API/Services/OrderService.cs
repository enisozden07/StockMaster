using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class OrderService
    {
        private readonly string? _connectionString;

        public OrderService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Orders";
                return await connection.QueryAsync<Order>(query);
            }
        }

        public async Task<Order?> GetOrderById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Orders WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Order>(query, new { Id = id });
            }
        }

        public async Task<Order> AddOrder(Order order)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Orders (OrderDate, ShippedDate, CustomerId) VALUES (@OrderDate, @ShippedDate, @CustomerId)";
                await connection.ExecuteAsync(query, order);
                return order;
            }
        }

        public async Task UpdateOrder(Order order)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Orders SET OrderDate = @OrderDate, ShippedDate = @ShippedDate, CustomerId = @CustomerId WHERE Id = @Id";
                await connection.ExecuteAsync(query, order);
            }
        }

        public async Task DeleteOrder(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Orders WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
