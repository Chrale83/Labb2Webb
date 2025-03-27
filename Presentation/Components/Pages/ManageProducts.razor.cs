using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.Dialogs;
using Presentation.DTOs;
using Presentation.Interfaces;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        public IProductService? ProductService { get; set; }

        [Inject]
        public AppState? appState { get; set; }

        private MudTable<ProductDTO>? mudTable;
        private List<ProductDTO> Products { get; set; } = new List<ProductDTO>();

        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        [Parameter]
        public ProductDTO? SelectedProduct { get; set; }

        [Parameter]
        public string searchText { get; set; } = string.Empty;
        private string message = string.Empty;
        private CancellationTokenSource? cts;
        private bool isProductDeleted = false;

        private async void SearchProducts(ChangeEventArgs input)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            try
            {
                await Task.Delay(600, cts.Token);
                var productQuery = input.Value?.ToString();

                var listHaveProducts = Products.Any();

                Products = await ProductService.SearchProductAsync(productQuery, listHaveProducts);
            }
            catch (TaskCanceledException)
            {
                //Sökningen avbröts eller så skrev för snabbt
            }
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            Categories = appState.Categories;
            Products = await ProductService.GetAllProductsAsync();
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                var productId = SelectedProduct.Id;
                await ProductService.DeleteProductByIDAsync(productId);
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

                Products = await ProductService.GetAllProductsAsync();
                SelectedProduct = null;
            }
        }
    }
}
