using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions
{
    public static class DtoToEntity
    {
        public static Product ProductDtoToEntity(this ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name.FormatString(),
                Description = productDto.Description.FormatString(),
                Price = productDto.Price,
                Status = productDto.Status,
                CategoryId = productDto.CategoryId,
            };

            return product;
        }
    }
}
