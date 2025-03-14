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
        private List<ProductFrontDto> Products { get; set; } = new List<ProductFrontDto>();
        private List<ProductFrontDto> SearchedProducts { get; set; } = new List<ProductFrontDto>();
        public IEnumerable<CategoryDtoApi> Categories { get; set; } = new List<CategoryDtoApi>();

        [Parameter]
        public ProductFrontDto? SelectedProduct { get; set; }

        [Parameter]
        public string searchText { get; set; } = string.Empty;
        private bool isSearching = false;

        private void SearchProducts(ChangeEventArgs input)
        {
            var textString = input.Value?.ToString();
            if (textString.Count() > 1)
            {
                SearchedProducts = Products
                    .Where(p => p.Name.Contains(textString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                isSearching = true;
            }
            else
            {
                isSearching = false;
            }
        }

        private void SearchProducts2(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length >= 2)
            {
                SearchedProducts = Products
                    .Where(p => p.Name.Contains(text, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                SearchedProducts.Clear();
            }
        }

        //private bool resetValueOnEmptyText;
        //private bool coerceText;
        //private bool coerceValue;
        //private bool selectedOnTab;
        //private string value2;

        //private async Task<IEnumerable<string>> SearchProduct(string value, CancellationToken token)
        //{
        //    if (string.IsNullOrWhiteSpace(value))
        //        return new List<string>();

        //    return Products
        //        .Where(p => p.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
        //        .Select(p => p.Name);
        //}

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
