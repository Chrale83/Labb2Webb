using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Extensions;
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
        public ProductUpdateDto? EditingProduct { get; set; }
        public ProductUpdateDto? OriginalProduct { get; set; }
        public List<CategoryDtoApi> Categories { get; set; } = new();
        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductUpdated = false;

        protected override void OnInitialized()
        {
            Categories = AppState.Categories;
            EditingProduct = new ProductUpdateDto(AppState.SelectedProduct);
            OriginalProduct = new ProductUpdateDto(AppState.SelectedProduct);

            //OriginalProduct = new ProductUpdateDto()
            //{
            //    Name = EditingProduct.Name,
            //    CategoryId = EditingProduct.CategoryId,
            //    Description = EditingProduct.Description,
            //    Price = EditingProduct.Price,
            //    Status = EditingProduct.Status,
            //};
        }

        private async Task HandleValidSubmit()
        {
            var updatedProduct = OriginalProduct.GetUpdatedFields(OriginalProduct);

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
