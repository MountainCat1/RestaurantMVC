

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using RestaurantMVC.Models;

namespace RestaurantMVC.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegistrationDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        public AccountService(RestaurantDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegistrationDto dto)
        {
            User newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email
            };

            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}
