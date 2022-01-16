using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using RestaurantMVC.Exceptions;
using RestaurantMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantMVC.Services
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> Get();
        public Task<OrderDto> Get(int id);
        public Task Delete(int id, ClaimsPrincipal claims);
        public Task Edit(OrderDto dto, ClaimsPrincipal claims);
        public Task Create(OrderDto dto, ClaimsPrincipal claims);
    }
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        public OrderService(RestaurantDbContext context, IMapper mapper, IAccountService accountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task Create(OrderDto dto, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Order entity = mapper.Map<Order>(dto);

                User user = accountService.GetUser(claims);

                await context.Orders.AddAsync(entity);


                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task Delete(int id, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Order entity = await context.Orders.FindAsync(id);

                if (entity == null)
                    throw new NotFoundException("");

                if (!Authorize(entity, claims))
                {
                    throw new ForbidException("");
                }

                context.Orders.Remove(entity);

                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task Edit(OrderDto dto, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Order entity = mapper.Map<Order>(dto);

                if (entity == null)
                    throw new NotFoundException("");

                if (!Authorize(entity, claims))
                {
                    throw new ForbidException("");
                }

                context.Orders.Update(entity);


                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task<List<OrderDto>> Get()
        { 
            List<Order> entity = await context.Orders
                .Include(x => x.Products)
                .ToListAsync();

            List<OrderDto> dto = mapper.Map<List<OrderDto>>(entity);

            return dto;
        }

        public async Task<OrderDto> Get(int id)
        {
            Order entity = await context.Orders
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new NotFoundException("");

            OrderDto dto = mapper.Map<OrderDto>(entity);

            return dto;
        }

        public bool Authorize(Order entity, ClaimsPrincipal claims)
        {
            User user = accountService.GetUser(claims);

            return user.RoleId == 1 || user.Id == entity.UserId;
        }
    }
}
