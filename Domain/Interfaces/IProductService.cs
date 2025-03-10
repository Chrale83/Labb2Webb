using Domain.Dtos;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
