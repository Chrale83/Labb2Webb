using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions
{
    public static class EntityToDto
    {
        public static IEnumerable<CategoryDto> CategoryToDto(this IEnumerable<Category> categories)
        {
            var result = new List<CategoryDto>();

            foreach (var category in categories)
            {
                result.Add(new CategoryDto { Id = category.Id, Name = category.Name });
            }

            return result;
        }
    }
}
