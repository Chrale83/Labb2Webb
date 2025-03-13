using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public HttpClient? Http { get; set; }

        [Inject]
        public AppState? appState { get; set; }

        [SupplyParameterFromForm]
        public ProductFrontDto? Product { get; set; }

        public List<CategoryDtoApi> Categories { get; set; } = new();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool ProductSaved = false;

        protected override async Task OnInitializedAsync()
        {
            ProductSaved = false;
            Product ??= new() { CategoryId = 1 };

            if (appState.Categories.Count == 0)
            {
                await appState.InitializeAsync(Http);
            }
            Categories = appState.Categories;
        }

        private void HandleInvalidSubmit()
        {
            statusClass = "alert-danger";
            message = "Check your forms";
            StateHasChanged();
        }

        private async Task HandleValidSubmit()
        {
            var response = await Http.PostAsJsonAsync("api/products", Product);
            statusClass = "alert-success";
            message = "Product added to database";
            ProductSaved = true;
        }
    }
}
