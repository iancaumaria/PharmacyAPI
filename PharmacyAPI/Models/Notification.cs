using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyAPI.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int UserId { get; set; } // Foreign Key către User

        [ForeignKey("UserId")]
        public User User { get; set; } // Proprietatea de navigare

        [Required]
        [MaxLength(255)]
        public string Message { get; set; } // Mesajul notificării

        [Required]
        public bool IsSent { get; set; } = false; // Starea notificării

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Data notificării
    }
}
