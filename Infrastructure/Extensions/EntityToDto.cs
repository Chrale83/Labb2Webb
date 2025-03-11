using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions
{
    public static class EntityToDto
    {
        public static IEnumerable<CategoryDto> CategoriesToDto(
            this IEnumerable<Category> categories
        )
        {
            var result = new List<CategoryDto>();

            foreach (var category in categories)
            {
                result.Add(new CategoryDto { Id = category.Id, Name = category.Name });
            }

            return result;
        }

        public static IEnumerable<ProductDto> ProductsToDto(this IEnumerable<Product> products)
        {
            var newList = new List<ProductDto>();

            foreach (var product in products)
            {
                newList.Add(
                    new ProductDto
                    {
                        Id = product.Id,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Name = product.Name,
                        Price = product.Price,
                        Status = product.Status,
                    }
                );
            }

            return newList;
        }
    }
}
