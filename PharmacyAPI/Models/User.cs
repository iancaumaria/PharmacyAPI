using System.ComponentModel.DataAnnotations;
namespace PharmacyAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Admin, Customer
        public ICollection<Order> Orders { get; set; }

    }
}
