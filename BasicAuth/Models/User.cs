using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAuth.Models
{
    public class User
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }

        [Column("email")]
        public string Email { get; set; }
        [Required]
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column("password")]
        public string PasswordHash { get; set; }
    }
}
