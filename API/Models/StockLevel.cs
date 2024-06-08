using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class StockLevel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
