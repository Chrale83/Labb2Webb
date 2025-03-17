using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        //Task<Product> CreateProductAsync(Product product);

        //Task<IEnumerable<Product>> GetProductsAsync();

        //Task<bool> DeleteProductAsync(int id);

        Task<IEnumerable<Product>> SearchProductsAsync(string searchWord);
    }
}
