using AutoMapper;
using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using RestaurantMVC.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Middleware;
using System.Security.Claims;
using RestaurantMVC.Exceptions;

namespace RestaurantMVC.Services
{
    public interface IProductService
    {
        public Task<List<ProductDto>> ProductGet();
        public Task<ProductDto> ProductGet(int id);
        public Task ProductDelete(int id, ClaimsPrincipal claims);
        public Task ProductEdit(ProductDto dto, ClaimsPrincipal claims);
        public Task ProductCreate(ProductDto dto, ClaimsPrincipal claims);
    }
    public class ProductService : IProductService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        public ProductService(RestaurantDbContext context, IMapper mapper, IAccountService accountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task ProductDelete(int id, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Product product = await context.Products.FindAsync(id);

                if (product == null)
                    throw new NotFoundException("");

                if (!Authorize(product, claims))
                {
                    throw new ForbidException("");
                }

                context.Products.Remove(product);

                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task ProductEdit(ProductDto dto, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Product product = mapper.Map<Product>(dto);

                if (!Authorize(product, claims)) {
                    throw new ForbidException("");
                }

                context.Products.Update(product);


                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task<List<ProductDto>> ProductGet()
        {
            List<Product> product = await context.Products.ToListAsync();

            List<ProductDto> productDto = mapper.Map<List<ProductDto>>(product);

            return productDto;
        }

        public async Task<ProductDto> ProductGet(int id)
        {
            Product product = await context.Products.FindAsync(id);

            ProductDto productDto = mapper.Map<ProductDto>(product);

            return productDto;
        }

        public bool Authorize(Product product, ClaimsPrincipal claims)
        {
            User user = accountService.GetUser(claims);

            return user.RoleId == 1;
        }

        public async Task ProductCreate(ProductDto dto, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Product product = mapper.Map<Product>(dto);

                User user = accountService.GetUser(claims);

                await context.Products.AddAsync(product);


                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }
    }
}
