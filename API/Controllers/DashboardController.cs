using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Services; 

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            var metrics = await _dashboardService.GetMetricsAsync();
            return Ok(metrics);
        }

        [HttpGet("recent-orders")]
        public async Task<IActionResult> GetRecentOrders()
        {
            var recentOrders = await _dashboardService.GetRecentOrdersAsync();
            return Ok(recentOrders);
        }

        [HttpGet("recent-shipments")]
        public async Task<IActionResult> GetRecentShipments()
        {
            var recentShipments = await _dashboardService.GetRecentShipmentsAsync();
            return Ok(recentShipments);
        }

        [HttpGet("product-distribution")]
        public async Task<IActionResult> GetProductDistribution()
        {
            var productDistribution = await _dashboardService.GetProductDistributionAsync();
            return Ok(productDistribution);
        }

        [HttpGet("sales-funnel")]
        public async Task<IActionResult> GetSalesFunnel()
        {
            var salesFunnel = await _dashboardService.GetSalesFunnelAsync();
            return Ok(salesFunnel);
        }
    }
}
