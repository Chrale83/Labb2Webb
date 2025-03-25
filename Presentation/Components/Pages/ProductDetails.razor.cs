using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        public IHttpClientFactory? httpClientFactory { get; set; }
        public HttpClient? httpClient { get; set; }

        [Inject]
        public AppState? appState { get; set; }

        public ProductDTO? SelectedProduct { get; set; }

        protected override void OnInitialized()
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
            SelectedProduct = appState.SelectedProduct;
        }
    }
}
