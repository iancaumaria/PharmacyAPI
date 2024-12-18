using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }


        // Relație cu tabela Categories
        public int CategoryId { get; set; }

        // Fă proprietatea de navigare opțională
        public Category? Category { get; set; }

        // Fă colecția opțională
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
