using Domain.Dtos;
using Domain.Entities;
using Presentation.DTOs;

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

        public static void UpdateFromDTO(this Product updated, ProductUpdateDto dto)
        {
            updated.Name = dto.Name;
            updated.Description = dto.Description;
            updated.Price = dto.Price;
            updated.CategoryId = dto.CategoryId;
            updated.Status = dto.Status;
        }
    }
}
