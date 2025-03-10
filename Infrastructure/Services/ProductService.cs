using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Extensions;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = productDto.ProductDtoToEntity();

            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return (await _productRepository.GetProductsAsync()).ProductsToDto();
        }
    }
}
