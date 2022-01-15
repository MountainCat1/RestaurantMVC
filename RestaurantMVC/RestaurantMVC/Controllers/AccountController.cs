using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using RestaurantMVC.Services;

namespace RestaurantMVC.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult ResgierUser([FromBody] RegistrationDto dto)
        {
            accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            string token = accountService.GenerateJwt(dto);
            //accountService.(dto);
            return Ok(token);
        }
    }
}
