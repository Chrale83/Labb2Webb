using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.DTOs;
using static MudBlazor.CategoryTypes;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        private IEnumerable<ProductFrontDto> Products { get; set; } = new List<ProductFrontDto>();
        public IEnumerable<CategoryDtoApi> Categories { get; set; } = new List<CategoryDtoApi>();
        private ProductFrontDto? selectedProduct;

        private void OnRowClick(TableRowClickEventArgs<ProductFrontDto> args)
        {
            selectedProduct = args.Item;
        }

        protected override async Task OnInitializedAsync()
        {
            Categories =
                await Http.GetFromJsonAsync<List<CategoryDtoApi>>("/api/categories") ?? new();
            Products = await Http.GetFromJsonAsync<List<ProductFrontDto>>("/api/products") ?? new();
        }

        private int selectedRowNumber = -1;
        private MudTable<ProductFrontDto> mudTable;
        private List<string> clickedEvents = new();

        private void RowClickEvent(TableRowClickEventArgs<ProductFrontDto> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        //private string SelectedRowClassFunc(ProductFrontDto product, int rowNumber)
        //{
        //    if (selectedRowNumber == rowNumber)
        //    {
        //        selectedRowNumber = -1;
        //        clickedEvents.Add("Selected Row: None");
        //        return string.Empty;
        //    }
        //    else if (mudTable.SelectedItem != null && mudTable.SelectedItem.Equals(product))
        //    {
        //        selectedRowNumber = rowNumber;
        //        clickedEvents.Add($"Selected Row: {rowNumber}");
        //        return "selected";
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}
    }
}
