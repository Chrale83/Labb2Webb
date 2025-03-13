using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class EditProduct
    {
        [Inject]
        public AppState appState { get; set; }

        [SupplyParameterFromForm]
        public ProductFrontDto? EditingProduct { get; set; }

        protected override void OnInitialized()
        {
            EditingProduct = appState.SelectedProduct;
            Categories = appState.Categories;
        }

        public List<CategoryDtoApi> Categories { get; set; } = new();
    }
}
