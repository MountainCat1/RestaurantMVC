using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RestaurantMVC.Models
{
    public class RegistrationDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
