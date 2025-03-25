using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.DTOs;
using Presentation.Models;
using Presentation.Services;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CartPage
    {
        [Inject]
        public ShoppingCartService? CartService { get; set; }

        [Inject]
        public IHttpClientFactory? httpClientFactory { get; set; }

        [Inject]
        public AppState? AppState { get; set; }
        public HttpClient? httpClient { get; set; }
        public MudTable<ShoppingCartItemModel>? mudTable { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public ShoppingCartItemModel? SelectedProduct { get; set; }
        private string totalSum;

        protected override void OnInitialized()
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
            Categories = AppState.Categories;
            CalcTotalSum();
        }

        private void RemoveFromCart()
        {
            CartService.RemoveFromCart(SelectedProduct.Product.Id);
            CalcTotalSum();
            SelectedProduct = null;
        }

        private void CalcTotalSum()
        {
            totalSum = CartService.Items.Sum(i => i.Product.Price * i.Quantity).ToString("C");
        }

        public async void SendOrderSubmit()
        {
            OrderDTO newOrder = new OrderDTO();

            foreach (var item in CartService.Items)
            {
                newOrder.ProductOrders.Add(
                    new ProductOrderDTO { ProductId = item.Product.Id, Quantity = item.Quantity }
                );
            }
        }
    }
}
