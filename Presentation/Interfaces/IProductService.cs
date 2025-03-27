using Presentation.DTOs;

namespace Presentation.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductDTO productDto);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task DeleteProductByIDAsync(int id);
        Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
        Task<List<ProductDTO>> SearchProductAsync(string searchWord, bool listHasProducts);
    }
}
