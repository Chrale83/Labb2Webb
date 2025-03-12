using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Extensions;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repository;

        public ProductService(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = productDto.ProductDtoToEntity();

            return await repository.AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return (await repository.GetAllAsync()).ProductsToDto();
        }
    }
}
