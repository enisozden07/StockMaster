using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class CustomerService
    {
        private readonly string? _connectionString;

        public CustomerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Customers";
                return await connection.QueryAsync<Customer>(query);
            }
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Customers WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
            }
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Customers (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)";
                await connection.ExecuteAsync(query, customer);
                return customer;
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Customers SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address WHERE Id = @Id";
                await connection.ExecuteAsync(query, customer);
            }
        }

        public async Task DeleteCustomer(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Customers WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
