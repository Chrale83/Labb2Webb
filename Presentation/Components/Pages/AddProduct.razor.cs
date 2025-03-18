using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [Inject]
        public AppState? AppState { get; set; }

        [SupplyParameterFromForm]
        public ProductDTO? Product { get; set; }

        public List<CategoryDTO> Categories { get; set; } = new();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductSaved = false;

        protected async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
            isProductSaved = false;
            Product ??= new() { CategoryId = 1 };

            if (AppState.Categories.Count == 0)
            {
                await AppState.InitializeAsync(_httpClient);
            }
            Categories = AppState.Categories;
        }

        //private void HandleInvalidSubmit()
        //{
        //    statusClass = "alert-danger";
        //    message = "Check your forms";
        //    StateHasChanged();
        //}

        private async Task HandleValidSubmit()
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", Product);
            statusClass = "alert-success";
            message = "Product added to database";
            isProductSaved = true;
        }
    }
}
