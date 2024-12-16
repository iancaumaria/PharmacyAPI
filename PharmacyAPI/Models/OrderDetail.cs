using System.ComponentModel.DataAnnotations;
namespace PharmacyAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; } // Legătură cu Order
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; } // Legătură cu Product
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; } // Cantitatea produsului în comandă
    }
}
