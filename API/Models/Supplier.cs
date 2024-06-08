using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string ContactInfo { get; set; } = string.Empty; 

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
