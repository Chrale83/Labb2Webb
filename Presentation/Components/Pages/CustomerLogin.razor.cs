using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Extensions;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CustomerLogin
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [Inject]
        public AppState AppState { get; set; }

        [Parameter]
        public CustomerLoginDTO LoginCustomer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoginCustomer = new();
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
        }

        private bool isRendered = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _httpClient.SetTokenToHttpClientFromLStorage(LocalStorage);

                var token = await LocalStorage.GetItemAsync<string>("authToken");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        token
                    );
                }

                isRendered = true;
                StateHasChanged(); // För att UI ska uppdateras
            }
        }

        private string message = "Inte testat än";

        private async Task CheckAuth()
        {
            var response = await _httpClient.GetAsync("api/auth");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                message = $"Serverns svar: {result}";
            }
            else
            {
                message = "Åtkomst nekad!";
            }
        }

        private async Task LoginSubmit()
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", LoginCustomer);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var customerData = JsonSerializer.Deserialize<CustomerLoginResponseDTO>(
                    responseString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                _httpClient.SaveTokenToLocalStorage(customerData.Token, LocalStorage);

                //await LocalStorage.SetItemAsync("authToken", customerData.Token);

                //// Lägg till token i HttpClient inför framtida anrop
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "Bearer",
                //    customerData.Token
                //);

                customerData.SaveLoginDataToState(AppState);

                //AppState.IsLoggedIn = true;
                AppState.OnChange += StateHasChanged;
                //Navigation.NavigateTo("/");
            }
            else
            {
                // Om det inte går att logga in... Fix later!
            }
        }
    }
}
