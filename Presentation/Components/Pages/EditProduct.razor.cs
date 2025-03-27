using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Interfaces;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class EditProduct
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public AppState AppState { get; set; }

        [SupplyParameterFromForm]
        public ProductDTO? SelectedProduct { get; set; }

        public List<CategoryDTO> Categories { get; set; } = new();
        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductUpdated = false;

        protected override void OnInitialized()
        {
            Categories = AppState.Categories;
            SelectedProduct = AppState.SelectedProduct;
        }

        private async Task UpdateProduct()
        {
            var editedProduct = new ProductUpdateDto(SelectedProduct);
            var productId = SelectedProduct.Id;

            if (SelectedProduct != null)
            {
                var response = await ProductService.UpdateProductAsync(productId, editedProduct);

                if (response)
                {
                    message = $"{SelectedProduct.Name} är uppdaterad i databasen";
                    statusClass = "alert-success";
                    isProductUpdated = true;
                }
                else
                {
                    message =
                        $"{SelectedProduct.Name} någpt gick fel vid uppdatering vid databasen";
                    statusClass = "alert-warning";
                    isProductUpdated = false;
                }
            }
        }
    }
}
