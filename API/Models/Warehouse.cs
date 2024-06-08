using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public string Manager { get; set; } = string.Empty; 

        public ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
    }
}
