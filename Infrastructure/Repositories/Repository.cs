using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(AppDbContext context)
        {
            dbContext = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete == null)
            {
                return false;
            }

            dbSet.Remove(entityToDelete);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public Task GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity enityt)
        {
            throw new NotImplementedException();
        }
    }
}
