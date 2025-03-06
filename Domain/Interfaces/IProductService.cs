using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
    }
}
