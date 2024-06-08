using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using API.Models;

namespace API.Services
{
    public class DashboardService
    {
        private readonly string? _connectionString;

        public DashboardService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DashboardData> GetDashboardDataAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var dashboardData = new DashboardData();

                var opportunitiesQuery = "SELECT SUM(Quantity * UnitPrice) FROM OrderDetails";
                dashboardData.Opportunities = await connection.QuerySingleAsync<decimal>(opportunitiesQuery);

                var revenueTotalQuery = "SELECT SUM(Quantity * UnitPrice) FROM OrderDetails od JOIN Orders o ON od.OrderId = o.Id WHERE o.ShippedDate IS NOT NULL";
                dashboardData.RevenueTotal = await connection.QuerySingleAsync<decimal>(revenueTotalQuery);

                var leadsQuery = "SELECT COUNT(*) FROM Customers";
                dashboardData.Leads = await connection.QuerySingleAsync<int>(leadsQuery);

                var totalOrdersQuery = "SELECT COUNT(*) FROM Orders";
                dashboardData.TotalOrders = await connection.QuerySingleAsync<int>(totalOrdersQuery);

                dashboardData.Conversion = dashboardData.Leads > 0 ? (int)((double)dashboardData.TotalOrders / dashboardData.Leads * 100) : 0;

                var revenueAnalysisQuery = @"
                    SELECT 
                        MONTH(OrderDate) AS Month, 
                        SUM(od.Quantity * od.UnitPrice) AS Revenue 
                    FROM Orders o 
                    JOIN OrderDetails od ON o.Id = od.OrderId 
                    GROUP BY MONTH(OrderDate)";
                dashboardData.RevenueAnalysis = (await connection.QueryAsync<RevenueAnalysis>(revenueAnalysisQuery)).ToList();

                dashboardData.ConversionFunnel = new List<ConversionFunnel>
                {
                    new ConversionFunnel 
                    { 
                        Stage = "Sales", 
                        Value = await connection.QuerySingleAsync<decimal>("SELECT SUM(Quantity * UnitPrice) FROM OrderDetails od JOIN Orders o ON od.OrderId = o.Id WHERE o.ShippedDate IS NOT NULL") 
                    },
                    new ConversionFunnel 
                    { 
                        Stage = "Quotes", 
                        Value = await connection.QuerySingleAsync<decimal>("SELECT SUM(Quantity * UnitPrice) FROM OrderDetails") 
                    },
                    new ConversionFunnel 
                    { 
                        Stage = "Leads", 
                        Value = dashboardData.Leads 
                    }
                };

                return dashboardData;
            }
        }
    }
}
