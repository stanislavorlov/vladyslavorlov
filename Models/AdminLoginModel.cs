using System.ComponentModel.DataAnnotations;

namespace VladyslavOrlovPromo.Models
{
    public class AdminLoginModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}