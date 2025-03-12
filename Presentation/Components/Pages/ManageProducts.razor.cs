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
        private ProductFrontDto? selectedProduct;

        //private void OnRowClick(TableRowClickEventArgs<ProductFrontDto> args)
        //{
        //    selectedProduct = args.Item;
        //}

        protected override async Task OnInitializedAsync()
        {
            Categories =
                await Http.GetFromJsonAsync<List<CategoryDtoApi>>("/api/categories") ?? new();
            Products = await Http.GetFromJsonAsync<List<ProductFrontDto>>("/api/products") ?? new();
        }

        private async Task DeleteProduct()
        {
            if (selectedProduct != null)
            {
                var response = await Http.DeleteAsync($"api/products/{selectedProduct.Id}");
                if (response.IsSuccessStatusCode) { }
                else { }
            }
        }

        private Task OpenDialogAsync()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };

            return DialogService.ShowAsync<ConfirmDeleteProductDialog>("Simple Dialog", options);
        }

        //private int selectedRowNumber = -1;
        //private List<string> clickedEvents = new();
    }
}
