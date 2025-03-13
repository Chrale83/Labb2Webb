using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class EditProduct
    {
        [Inject]
        public AppState AppState { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [SupplyParameterFromForm]
        public ProductFrontDto? EditingProduct { get; set; }
        public List<CategoryDtoApi> Categories { get; set; } = new();
        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductUpdated = false;

        protected override void OnInitialized()
        {
            EditingProduct = AppState.SelectedProduct;
            Categories = AppState.Categories;
        }

        private async Task HandleValidSubmit()
        {
            if (EditingProduct != null)
            {
                var response = await Http.PatchAsJsonAsync(
                    $"/api/products/{EditingProduct.Id}",
                    EditingProduct
                );

                if (response.IsSuccessStatusCode)
                {
                    message = $"{EditingProduct.Name} was updated in database";
                    statusClass = "alert-success";
                    isProductUpdated = true;
                }
                else
                {
                    message = $"{EditingProduct.Name} was not updated";
                    statusClass = "alert-warning";
                    isProductUpdated = false;
                }
            }
        }
    }
}
