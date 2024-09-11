using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterAPI.Entities
{
    public class User
    {
        [Key]
        [StringLength(25)]
        public string UserId { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage ="Mobile number must be exactly 10 digits.")]
        public string MobileNumber { get; set; }
        [Column(TypeName = "Date")]
        public DateTime JoinedAt { get; set; } = DateTime.Now;
        public string? Role { get; set; }
    }
}
