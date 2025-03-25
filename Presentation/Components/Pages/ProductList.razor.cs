using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.Dialogs;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ProductList
    {
        [Inject]
        public IHttpClientFactory? httpClientFactory { get; set; }
        public HttpClient? httpClient { get; set; }

        [Inject]
        public AppState? appState { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        [SupplyParameterFromForm]
        public List<ProductDTO> Products { get; set; } = new();
        public ProductDTO? SelectedProduct { get; set; }
        private MudTable<ProductDTO>? mudTable;

        protected override async Task OnInitializedAsync()
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
            Products =
                await httpClient.GetFromJsonAsync<List<ProductDTO>>("/api/products") ?? new();

            if (appState.Categories.Count == 0)
            {
                await appState.InitializeAsync(httpClient);
            }
            Categories = appState.Categories;
        }

        public void OnRowClick(TableRowClickEventArgs<ProductDTO> rowClickEventArgs)
        {
            appState.SelectedProduct = SelectedProduct;
            var id = rowClickEventArgs.Item.Id;
            navigationManager.NavigateTo("/productdetail");
            //navigationManager.NavigateTo($"/productdetail");
        }
    }
}
