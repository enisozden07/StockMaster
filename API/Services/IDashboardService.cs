namespace API.Services
{
    public interface IDashboardService
    {
        Task<DashboardMetrics> GetMetricsAsync();
        Task<IEnumerable<RecentOrder>> GetRecentOrdersAsync();
        Task<IEnumerable<RecentShipment>> GetRecentShipmentsAsync();
        Task<IEnumerable<ProductDistribution>> GetProductDistributionAsync();
        Task<IEnumerable<SalesFunnelStage>> GetSalesFunnelAsync();
    }
}
