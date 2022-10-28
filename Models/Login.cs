using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
