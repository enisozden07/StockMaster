using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        [Required]
        public DateTime ShipmentDate { get; set; }

        public string TrackingNumber { get; set; } = string.Empty;

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
