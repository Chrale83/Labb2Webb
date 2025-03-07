using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReferenceDataRepository : IReferenceDataRepository
    {
        private readonly AppDbContext _context;

        public ReferenceDataRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Category>> GetCategoriesAsync()
        //{
        //    return await _context.Categories.ToListAsync();
        //}

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            (await _context.Categories.ToListAsync());
    }
}
