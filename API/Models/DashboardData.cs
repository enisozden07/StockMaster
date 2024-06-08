public class DashboardData
{
    public decimal Opportunities { get; set; }
    public decimal RevenueTotal { get; set; }
    public int Conversion { get; set; }
    public int Leads { get; set; }
    public int TotalOrders { get; set; } // Add this property
    public List<RevenueAnalysis> RevenueAnalysis { get; set; } = new List<RevenueAnalysis>();
    public List<ConversionFunnel> ConversionFunnel { get; set; } = new List<ConversionFunnel>();
    public List<RevenueSnapshot> RevenueSnapshot { get; set; } = new List<RevenueSnapshot>();
}

public class RevenueAnalysis
{
    public string Month { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
}

public class ConversionFunnel
{
    public string Stage { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class RevenueSnapshot
{
    public string Category { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
