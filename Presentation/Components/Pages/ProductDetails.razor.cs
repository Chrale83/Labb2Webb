using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Services;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        public IHttpClientFactory? httpClientFactory { get; set; }
        public HttpClient? httpClient { get; set; }

        [Inject]
        public SharedState? appState { get; set; }

        [Inject]
        public ShoppingCartService? CartService { get; set; }

        public ProductDTO? SelectedProduct { get; set; }
        public List<int>? SelectedQuantityOfProducts { get; set; } =
            Enumerable.Range(1, 10).ToList();
        public int SelectedQuantity { get; set; } = 1;
        private bool IsAddedToCart = false;

        protected override void OnInitialized()
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
            SelectedProduct = appState.SelectedProduct;
        }

        private void SelectQuantity(int quantity)
        {
            SelectedQuantity = quantity;
        }

        private void PutInCart()
        {
            CartService.AddToCart(SelectedProduct, SelectedQuantity);
            IsAddedToCart = true;
        }
    }
}
