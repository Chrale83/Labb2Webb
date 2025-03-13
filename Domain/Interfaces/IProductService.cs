using Domain.Dtos;
using Domain.Entities;
using Presentation.DTOs;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<bool> DeleteProductAsync(int id);

        Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);

        //Task UpdateProduct(int id, Dictionary<string, object> updatedProduct);
    }
}
