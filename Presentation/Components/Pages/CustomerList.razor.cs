using System.Net;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Extensions;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CustomerList
    {
        [Inject]
        public AppState? AppState { get; set; }

        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        public IHttpClientFactory? httpClientFactory { get; set; }
        public HttpClient? httpClient { get; set; }
        public List<CustomerProfileModel>? CustomerProfiles { get; set; } = new();
        public CustomerProfileModel? SelectedCustomer { get; set; } = new();

        [Parameter]
        public string SearchText { get; set; } = string.Empty;
        private MudTable<CustomerProfileModel>? mudTable;
        private string message = string.Empty;

        //private bool firstRender = true;

        protected override void OnInitialized()
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await httpClient.SetTokenToHttpClientFromLStorage(LocalStorage);
                CustomerProfiles = await httpClient.GetFromJsonAsync<List<CustomerProfileModel>>(
                    "/api/customers"
                );

                firstRender = false;
                StateHasChanged();
            }
        }

        private async Task SearchForCustomersName(ChangeEventArgs input)
        {
            var searchWord = input.Value?.ToString();

            await Task.Delay(600);

            var counterChar = searchWord.Length;

            switch (counterChar)
            {
                case 0:
                    CustomerProfiles = await httpClient.GetFromJsonAsync<
                        List<CustomerProfileModel>
                    >("/api/customers");
                    break;
                case >= 2:
                    await ResponseSearchProducts(searchWord);

                    break;
            }
            StateHasChanged();
        }

        private async Task ResponseSearchProducts(string searchWord)
        {
            var response = await httpClient.GetAsync(
                $"/api/customers/search?searchWord={searchWord}"
            );

            if (response.StatusCode == HttpStatusCode.OK)
            {
                CustomerProfiles =
                    await response.Content.ReadFromJsonAsync<List<CustomerProfileModel>>() ?? new();
                message = string.Empty;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                CustomerProfiles = new();
                message = "No products found";
            }
        }
    }
}
