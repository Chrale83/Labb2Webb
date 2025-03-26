using Blazored.LocalStorage;
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
        private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        [SupplyParameterFromForm]
        public ProductDTO? SelectedProduct { get; set; }

        //public ProductUpdateDto? OriginalProduct { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new();
        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductUpdated = false;

        protected override void OnInitialized()
        {
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
            Categories = AppState.Categories;
            SelectedProduct = AppState.SelectedProduct;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && AppState.UserInfo.Role == "ADMIN")
            {
                await _httpClient.SetTokenToHttpClientFromLStorage(LocalStorage);
            }
        }

        private async Task HandleValidSubmit()
        {
            var editedProduct = new ProductUpdateDto(SelectedProduct);
            //var updatedProduct = OriginalProduct.GetUpdatedFields(editedProduct);

            if (SelectedProduct != null)
            {
                var response = await _httpClient.PutAsJsonAsync(
                    $"/api/products/{SelectedProduct.Id}",
                    editedProduct
                );

                if (response.IsSuccessStatusCode)
                {
                    message = $"{SelectedProduct.Name} was updated in database";
                    statusClass = "alert-success";
                    isProductUpdated = true;
                }
                else
                {
                    message = $"{SelectedProduct.Name} was not updated";
                    statusClass = "alert-warning";
                    isProductUpdated = false;
                }
            }
        }
    }
}
