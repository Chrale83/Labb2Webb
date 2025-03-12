namespace Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity enityt);
        Task<bool> DeleteAsync(int id);
    }
}
