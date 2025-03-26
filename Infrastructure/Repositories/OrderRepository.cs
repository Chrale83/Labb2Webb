using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Presentation.DTOs;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task CreateOrder(Order order, int customerId)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }
    }
}
