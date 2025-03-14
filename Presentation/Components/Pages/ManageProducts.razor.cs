using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.Dialogs;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [Inject]
        public AppState appState { get; set; }

        private MudTable<ProductFrontDto>? mudTable;
        private IEnumerable<ProductFrontDto> Products { get; set; } = new List<ProductFrontDto>();
        public IEnumerable<CategoryDtoApi> Categories { get; set; } = new List<CategoryDtoApi>();

        [Parameter]
        public ProductFrontDto? SelectedProduct { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
            if (appState.Categories.Count == 0)
            {
                await appState.InitializeAsync(_httpClient);
            }
            Categories = appState.Categories;

            Products =
                await _httpClient.GetFromJsonAsync<List<ProductFrontDto>>("/api/products") ?? new();
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                var response = await _httpClient.DeleteAsync($"api/products/{SelectedProduct.Id}");
                if (response.IsSuccessStatusCode) { }
                else { }
            }
        }

        private async Task EditProduct()
        {
            appState.SelectedProduct = SelectedProduct;
            Navigation.NavigateTo("/editproductpages");
        }

        private async Task OpenConfirmDialogDelete()
        {
            var parameters = new DialogParameters { ["ProductName"] = SelectedProduct.Name };

            var options = new DialogOptions { CloseOnEscapeKey = true };

            var response = await this.DialogService.ShowAsync<ConfirmDeleteProductDialog>(
                "Simple Dialog",
                parameters,
                options
            );

            var result = await response.Result;

            if (!result.Canceled)
            {
                await DeleteProduct();
                Products = await _httpClient.GetFromJsonAsync<List<ProductFrontDto>>(
                    "/api/products"
                );
                SelectedProduct = null;
            }
        }
    }
}
