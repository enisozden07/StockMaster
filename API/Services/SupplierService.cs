using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class SupplierService
    {
        private readonly string? _connectionString;

        public SupplierService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Suppliers";
                return await connection.QueryAsync<Supplier>(query);
            }
        }

        public async Task<Supplier?> GetSupplierById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Suppliers WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Supplier>(query, new { Id = id });
            }
        }

        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Suppliers (Name, ContactInfo) VALUES (@Name, @ContactInfo)";
                await connection.ExecuteAsync(query, supplier);
                return supplier;
            }
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Suppliers SET Name = @Name, ContactInfo = @ContactInfo WHERE Id = @Id";
                await connection.ExecuteAsync(query, supplier);
            }
        }

        public async Task DeleteSupplier(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Suppliers WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
