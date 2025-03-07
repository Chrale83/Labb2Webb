using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReferenceDataRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
