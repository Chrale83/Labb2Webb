using Domain.Dtos;
using Domain.Interfaces;
using Infrastructure.Extensions;

namespace Infrastructure.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IReferenceDataRepository _referenceDataRepository;

        public ReferenceDataService(IReferenceDataRepository referenceDataRepository)
        {
            _referenceDataRepository = referenceDataRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return (await _referenceDataRepository.GetCategoriesAsync()).CategoryToDto();
        }
    }
}
