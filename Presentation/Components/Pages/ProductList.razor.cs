using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.DTOs;
using Presentation.Interfaces;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ProductList
    {
        [Inject]
        public IProductService? ProductService { get; set; }

        [Inject]
        public SharedState? appState { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        [SupplyParameterFromForm]
        public List<ProductDTO> Products { get; set; } = new();
        public ProductDTO? SelectedProduct { get; set; }
        private MudTable<ProductDTO>? mudTable;
        private CancellationTokenSource? cts;

        [Parameter]
        public string searchText { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetAllProductsAsync();
            Categories = appState.Categories;
        }

        public void OnRowClick(TableRowClickEventArgs<ProductDTO> rowClickEventArgs)
        {
            appState.SelectedProduct = SelectedProduct;
            navigationManager.NavigateTo("/productdetail");
        }

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
    }
}
