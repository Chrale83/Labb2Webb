﻿using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.DTOs;
using Presentation.Extensions;
using Presentation.Interfaces;
using Presentation.Models;
using Presentation.Services;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CartPage
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public ShoppingCartService? CartService { get; set; }

        [Inject]
        public SharedState? AppState { get; set; }

        public MudTable<ShoppingCartItemModel>? mudTable { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public ShoppingCartItemModel? SelectedProduct { get; set; }
        private decimal totalSum;
        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isOrderSent = false;

        protected override void OnInitialized()
        {
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
            totalSum = CartService.Items.Sum(i => i.Product.Price * i.Quantity);
        }

        public async void SendOrderSubmit()
        {
            OrderDTO newOrder = new OrderDTO();
            newOrder.TotalCost = totalSum;

            foreach (var item in CartService.Items)
            {
                newOrder.ProductOrders.Add(
                    new ProductOrderDTO { ProductId = item.Product.Id, Quantity = item.Quantity }
                );
            }

            var response = await OrderService.CreateOrder(newOrder);

            if (response)
            {
                statusClass = "alert-success";
                message = "Produkten är tillagd i listan";
                isOrderSent = true;
                CartService.ClearCart();
                NavigationManager.NavigateTo("confirmationpage");
            }
            else
            {
                message = "Nåt gick fel";
                statusClass = "alert-danger";
            }
        }
    }
}
