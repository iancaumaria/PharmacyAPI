using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int OrderId { get; set; } // Foreign Key către Order

        [ForeignKey("OrderId")]
        public Order? Order { get; set; } // Proprietatea de navigare

        [Required]
        public int ProductId { get; set; } // Foreign Key către Product

        [ForeignKey("ProductId")]
        public Product? Product { get; set; } // Proprietatea de navigare

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } // Cantitatea produsului în comandă
    }
}
