using Presentation.DTOs;

namespace Presentation.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductDTO productDto);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task DeleteProductByIDAsync(int id);
        Task UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
        Task<IEnumerable<ProductDTO>> SearchProductAsync(string searchWord);
    }
}
