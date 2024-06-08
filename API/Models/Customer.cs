using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
