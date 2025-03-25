using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Extensions;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        public AppState? AppState { get; set; }

        [SupplyParameterFromForm]
        public ProductDTO? Product { get; set; }

        public List<CategoryDTO> Categories { get; set; } = new();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductSaved = false;
        protected bool firstRender = true;

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
            isProductSaved = false;
            Product ??= new() { CategoryId = 1 };

            if (AppState.Categories.Count == 0)
            {
                await AppState.InitializeAsync(_httpClient);
            }
            Categories = AppState.Categories;

            await _httpClient.SetTokenToHttpClientFromLStorage(LocalStorage);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _httpClient.SetTokenToHttpClientFromLStorage(LocalStorage);
                firstRender = true;
            }
        }

        private void HandleInvalidSubmit()
        {
            statusClass = "alert-danger";
            message = "Check your forms";
        }

        private async Task HandleValidSubmit()
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", Product);
            if (response.IsSuccessStatusCode)
            {
                statusClass = "alert-success";
                message = "Produkten är tillagd i listan";
                isProductSaved = true;
            }

            message = "Nåt gick fel";
            statusClass = "alert-danger";
        }
    }
}
