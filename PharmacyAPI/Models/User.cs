using System;
using System.Collections.Generic;
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
        public string PasswordHash { get; set; } // Stochează hash-ul parolei, nu parola brută

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; } // Ex: "Admin", "Customer"

        // Relația cu entitatea Orders
        public ICollection<Order> Orders { get; set; }

        // Proprietăți suplimentare pentru urmărirea utilizatorilor
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
