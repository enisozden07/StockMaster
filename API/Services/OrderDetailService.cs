using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class OrderDetailService
    {
        private readonly string? _connectionString;

        public OrderDetailService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM OrderDetails";
                return await connection.QueryAsync<OrderDetail>(query);
            }
        }

        public async Task<OrderDetail?> GetOrderDetailById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM OrderDetails WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<OrderDetail>(query, new { Id = id });
            }
        }

        public async Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice) VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)";
                await connection.ExecuteAsync(query, orderDetail);
                return orderDetail;
            }
        }

        public async Task UpdateOrderDetail(OrderDetail orderDetail)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE OrderDetails SET OrderId = @OrderId, ProductId = @ProductId, Quantity = @Quantity, UnitPrice = @UnitPrice WHERE Id = @Id";
                await connection.ExecuteAsync(query, orderDetail);
            }
        }

        public async Task DeleteOrderDetail(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM OrderDetails WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
