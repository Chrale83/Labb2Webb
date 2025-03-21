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
        private MudTable<CustomerProfileModel>? mudTable;

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
                    "/api/customer"
                );

                firstRender = false;
                StateHasChanged();
            }
        }
    }
}
