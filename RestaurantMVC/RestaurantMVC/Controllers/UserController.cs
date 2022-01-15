using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Register([FromForm] RegistrationDto registrationDto)
        {
            return Ok($"{registrationDto.Username}, {registrationDto.Email}, {registrationDto.Password}");
        }

        public IActionResult Login([FromForm] LoginDto registrationDto)
        {
            return Ok($"{registrationDto.Username}, {registrationDto.Email}, {registrationDto.Password}");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
