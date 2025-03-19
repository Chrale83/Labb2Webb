using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Extensions;
using Presentation.DTOs;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IProductRepository _productRepository;

        public ProductService(IRepository<Product> repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = productDto.ProductDtoToEntity();

            return await _repository.AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return (await _repository.GetAllAsync()).ProductsToDto();
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string search)
        {
            var formatedSearch = search.ToLower().Replace(" ", "");
            return (await _productRepository.SearchProductsAsync(formatedSearch)).ProductsToDto();
        }

        public async Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
        {
            var productToUpdate = await _repository.GetByIdAsync(id);
            if (productToUpdate == null)
            {
                return false;
            }

            productToUpdate.UpdateFromDTO(productUpdateDto);

            return await _repository.UpdateAsync(productToUpdate);
        }

        //Task IProductService.UpdateProduct(int id, Dictionary<string, object> updatedProduct)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
