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

        public async Task<List<Order>> GetOrdersForCustomer(int customerId)
        {
            //var order = await context.Orders.Where(o => o.CustomerId == customerId).Include(op => op.OrderProducts).ThenInclude(a => a.)
            var orders = await context
                .Orders.Where(o => o.CustomerId == customerId)
                .Include(b => b.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(p => p.Category)
                .Include(o => o.Customer)
                .ToListAsync();

            return orders;
        }
    }
}
