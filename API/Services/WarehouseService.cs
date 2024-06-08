using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class WarehouseService
    {
        private readonly string? _connectionString;

        public WarehouseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouses()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Warehouses";
                return await connection.QueryAsync<Warehouse>(query);
            }
        }

        public async Task<Warehouse?> GetWarehouseById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Warehouses WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Warehouse>(query, new { Id = id });
            }
        }

        public async Task<Warehouse> AddWarehouse(Warehouse warehouse)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Warehouses (Location, Manager) VALUES (@Location, @Manager)";
                await connection.ExecuteAsync(query, warehouse);
                return warehouse;
            }
        }

        public async Task UpdateWarehouse(Warehouse warehouse)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Warehouses SET Location = @Location, Manager = @Manager WHERE Id = @Id";
                await connection.ExecuteAsync(query, warehouse);
            }
        }

        public async Task DeleteWarehouse(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Warehouses WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
