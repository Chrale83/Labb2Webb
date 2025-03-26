using System.Net.Http;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.Extensions;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CustomerEdit
    {
        [Inject]
        public AppState? AppState { get; set; }

        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        public IHttpClientFactory? httpClientFactory { get; set; }
        public HttpClient? httpClient { get; set; }
        public CustomerEditModel? EditedCustomer { get; set; } = new();
        private bool firstRender = true;
        private string message = "string.Empty";

        protected override void OnInitialized()
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await httpClient.SetTokenToHttpClientFromLStorage(LocalStorage);
                EditedCustomer = await httpClient.GetFromJsonAsync<CustomerEditModel>(
                    $"/api/customers/{AppState.UserInfo.UserId}"
                );

                StateHasChanged();
            }
        }

        private async Task EditCustomerSubmit()
        {
            await httpClient.PutAsJsonAsync($"/api/customers/", EditedCustomer);
            message = "Ändringar har sparats";
            StateHasChanged();
            await Task.Delay(3000);
            message = string.Empty;
        }

        private async Task UndoCustomerEditSubmit()
        {
            //Denna ska hämta igen
            EditedCustomer = await httpClient.GetFromJsonAsync<CustomerEditModel>(
                $"/api/customers/{AppState.UserInfo.UserId}"
            );
            message = "uppgifter har återställts";
            StateHasChanged();
            await Task.Delay(3000);
            message = string.Empty;
        }
    }
}
