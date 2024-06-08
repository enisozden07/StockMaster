namespace API.Models
{
    public class ShipmentDto
    {
        public int Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
    }
}
