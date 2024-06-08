using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class ShipmentService
    {
        private readonly string? _connectionString;

        public ShipmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Shipment>> GetShipments()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Shipments";
                return await connection.QueryAsync<Shipment>(query);
            }
        }

        public async Task<Shipment?> GetShipmentById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Shipments WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Shipment>(query, new { Id = id });
            }
        }

        public async Task<Shipment> AddShipment(Shipment shipment)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO Shipments (OrderId, ShipmentDate, TrackingNumber, SupplierId) VALUES (@OrderId, @ShipmentDate, @TrackingNumber, @SupplierId)";
                await connection.ExecuteAsync(query, shipment);
                return shipment;
            }
        }

        public async Task UpdateShipment(Shipment shipment)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Shipments SET OrderId = @OrderId, ShipmentDate = @ShipmentDate, TrackingNumber = @TrackingNumber, SupplierId = @SupplierId WHERE Id = @Id";
                await connection.ExecuteAsync(query, shipment);
            }
        }

        public async Task DeleteShipment(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM Shipments WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
