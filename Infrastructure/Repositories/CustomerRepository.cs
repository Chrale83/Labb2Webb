using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<IEnumerable<Customer>> SearchForCustomer(string searchedText)
        {
            return await context.Customers.Where(c => c.Email.Contains(searchedText)).ToListAsync();
        }
    }
}
