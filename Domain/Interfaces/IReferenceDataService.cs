using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface IReferenceDataService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
