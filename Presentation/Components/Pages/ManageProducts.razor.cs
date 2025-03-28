﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.Dialogs;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        private IHttpClientFactory? HttpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [Inject]
        public AppState? appState { get; set; }

        private MudTable<ProductDTO>? mudTable;
        private List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        private List<ProductDTO> SearchedProducts { get; set; } = new List<ProductDTO>();
        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        [Parameter]
        public ProductDTO? SelectedProduct { get; set; }

        [Parameter]
        public string searchText { get; set; } = string.Empty;
        private string message = string.Empty;

        private async void SearchProducts(ChangeEventArgs input)
        {
            var searchWord = input.Value?.ToString();

            await Task.Delay(1000);

            var counterChar = searchWord.Length;

            switch (counterChar)
            {
                case 0:
                    Products =
                        await _httpClient.GetFromJsonAsync<List<ProductDTO>>("/api/products")
                        ?? new();
                    break;
                case >= 2:
                    await ResponseSearchProducts(searchWord);

                    break;
            }
            StateHasChanged();
        }

        public async Task ResponseSearchProducts(string searchWord)
        {
            var response = await _httpClient.GetAsync(
                $"/api/products/search?searchWord={searchWord}"
            );

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>() ?? new();
                message = string.Empty;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Products = new();
                message = "Ingen produkt hittades";
            }
            else { }
        }

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
            if (appState.Categories.Count == 0)
            {
                await appState.InitializeAsync(_httpClient);
            }
            Categories = appState.Categories;

            Products =
                await _httpClient.GetFromJsonAsync<List<ProductDTO>>("/api/products") ?? new();
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                var token = await LocalStorage.GetItemAsync<string>("authToken");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        token
                    );
                }

                var response = await _httpClient.DeleteAsync($"api/products/{SelectedProduct.Id}");
                if (response.IsSuccessStatusCode) { }
                else { }
            }
        }

        private async Task EditProduct()
        {
            appState.SelectedProduct = SelectedProduct;
            Navigation.NavigateTo("/editproductpages");
        }

        private async Task OpenConfirmDialogDelete()
        {
            var parameters = new DialogParameters { ["ProductName"] = SelectedProduct.Name };

            var options = new DialogOptions { CloseOnEscapeKey = true };

            var response = await this.DialogService.ShowAsync<ConfirmDeleteProductDialog>(
                "Simple Dialog",
                parameters,
                options
            );

            var result = await response.Result;

            if (!result.Canceled)
            {
                await DeleteProduct();
                Products = await _httpClient.GetFromJsonAsync<List<ProductDTO>>("/api/products");
                SelectedProduct = null;
            }
        }
    }
}
