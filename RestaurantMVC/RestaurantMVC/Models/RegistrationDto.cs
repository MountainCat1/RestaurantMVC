using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RestaurantMVC.Models
{
    public class RegistrationDto
    {
        [MinLength(3, ErrorMessage = "Username length must be between 3-32")]
        [MaxLength(32, ErrorMessage = "Username length must be between 3-32")]
        [Required]
        //[StringLength(32, MinimumLength = 3,  ErrorMessage = "Username length must less than 32")]
        public string Username { get; set; }

        //[MinLength(1, ErrorMessage = "Name length must be between 1-32")]
        //[MaxLength(32, ErrorMessage = "Name length must be between 1-32")]
        [Required]
        [EmailAddress(ErrorMessage ="UwU")]
        public string Email { get; set; }

        [MaxLength(32, ErrorMessage = "Password length must be between 1-32")]
        [MinLength(6, ErrorMessage = "Password length must be between 6-32")]
        [Required]
        //[StringLength(32, MinimumLength = 3, ErrorMessage = "Password length must less than 32")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
    }
}
