using Blazored.LocalStorage;
using Presentation.DTOs;
using Presentation.Extensions;
using Presentation.Interfaces;

namespace Presentation.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private const string uri = "api/products";

        public ProductService(
            IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService
        )
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
            localStorage = localStorageService;
        }

        public async Task<bool> CreateProductAsync(ProductDTO productDto)
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            var response = await httpClient.PostAsJsonAsync($"{uri}", productDto);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task DeleteProductByIDAsync(int id)
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            await httpClient.DeleteAsync($"{uri}/{id}");
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>($"{uri}");
        }

        public async Task<IEnumerable<ProductDTO>> SearchProductAsync(string productQuery)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>(
                $"{uri}/search?{productQuery}"
            );
        }

        public async Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            var response = await httpClient.PutAsJsonAsync($"{uri}/{id}", productUpdateDto);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
