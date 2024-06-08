using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class StockLevelService
    {
        private readonly string? _connectionString;

        public StockLevelService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<StockLevel>> GetStockLevels()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM StockLevels";
                return await connection.QueryAsync<StockLevel>(query);
            }
        }

        public async Task<StockLevel?> GetStockLevelById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM StockLevels WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<StockLevel>(query, new { Id = id });
            }
        }

        public async Task<StockLevel> AddStockLevel(StockLevel stockLevel)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO StockLevels (ProductId, WarehouseId, Quantity) VALUES (@ProductId, @WarehouseId, @Quantity)";
                await connection.ExecuteAsync(query, stockLevel);
                return stockLevel;
            }
        }

        public async Task UpdateStockLevel(StockLevel stockLevel)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE StockLevels SET ProductId = @ProductId, WarehouseId = @WarehouseId, Quantity = @Quantity WHERE Id = @Id";
                await connection.ExecuteAsync(query, stockLevel);
            }
        }

        public async Task DeleteStockLevel(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM StockLevels WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
