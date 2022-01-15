using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RestaurantMVC.Models
{
    public class RegistrationDto
    {
        [MinLength(3, ErrorMessage = "Username length must be between 3-32")]
        [MaxLength(32, ErrorMessage = "Username length must be between 3-32")]
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="UwU")]
        public string Email { get; set; }

        [MaxLength(32, ErrorMessage = "Password length must be between 1-32")]
        [MinLength(6, ErrorMessage = "Password length must be between 6-32")]
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
    }
}
