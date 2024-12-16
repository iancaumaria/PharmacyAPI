using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PharmacyAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int UserId { get; set; } // Legătură cu User
        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
