namespace Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(TEntity entity);
    }
}
