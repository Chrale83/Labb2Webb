using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.Dialogs;
using Presentation.DTOs;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        private MudTable<ProductFrontDto>? mudTable;
        private IEnumerable<ProductFrontDto> Products { get; set; } = new List<ProductFrontDto>();
        public IEnumerable<CategoryDtoApi> Categories { get; set; } = new List<CategoryDtoApi>();

        [Parameter]
        public ProductFrontDto? SelectedProduct { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Categories =
                await Http.GetFromJsonAsync<List<CategoryDtoApi>>("/api/categories") ?? new();
            Products = await Http.GetFromJsonAsync<List<ProductFrontDto>>("/api/products") ?? new();
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                var response = await Http.DeleteAsync($"api/products/{SelectedProduct.Id}");
                if (response.IsSuccessStatusCode) { }
                else { }
            }
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
                Products = await Http.GetFromJsonAsync<List<ProductFrontDto>>("/api/products");
                SelectedProduct = null;
            }
        }
    }
}
