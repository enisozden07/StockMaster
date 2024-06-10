using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly string _connectionString;

        public DashboardService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(_connectionString));
        }

        public async Task<DashboardMetrics> GetMetricsAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var totalProducts = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Products");
                var totalOrders = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Orders");
                var totalCustomers = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Customers");
                var totalShipments = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Shipments");

                return new DashboardMetrics
                {
                    TotalProducts = totalProducts,
                    TotalOrders = totalOrders,
                    TotalCustomers = totalCustomers,
                    TotalShipments = totalShipments
                };
            }
        }

        public async Task<IEnumerable<RecentOrder>> GetRecentOrdersAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<RecentOrder>("SELECT o.Id, o.OrderDate, c.Name AS CustomerName FROM Orders o JOIN Customers c ON o.CustomerId = c.Id ORDER BY o.OrderDate DESC LIMIT 5");
            }
        }

        public async Task<IEnumerable<RecentShipment>> GetRecentShipmentsAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<RecentShipment>("SELECT s.Id, s.ShipmentDate, s.TrackingNumber FROM Shipments s ORDER BY s.ShipmentDate DESC LIMIT 5");
            }
        }

        public async Task<IEnumerable<ProductDistribution>> GetProductDistributionAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<ProductDistribution>("SELECT p.Name AS ProductName, SUM(od.Quantity) AS Quantity FROM OrderDetails od JOIN Products p ON od.ProductId = p.Id GROUP BY p.Name");
            }
        }

        public async Task<IEnumerable<SalesFunnelStage>> GetSalesFunnelAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<SalesFunnelStage>("SELECT Stage, COUNT(*) AS Count FROM SalesFunnel GROUP BY Stage");
            }
        }
    }

    public class DashboardMetrics
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalShipments { get; set; }
    }

    public class RecentOrder
    {
        public int Id { get; set; }
        public string OrderDate { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
    }

    public class RecentShipment
    {
        public int Id { get; set; }
        public string ShipmentDate { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
    }

    public class ProductDistribution
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class SalesFunnelStage
    {
        public string Stage { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
