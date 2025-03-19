using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchText)
        {
            return await _context
                .Products.Where(n => n.Name.Replace(" ", "").Contains(searchText))
                .ToListAsync();
        }
    }
}
