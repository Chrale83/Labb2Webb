using System.Net.Http.Headers;
using System.Net.NetworkInformation;
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
        public SharedState AppState { get; set; }

        [Parameter]
        public CustomerLoginDTO LoginCustomer { get; set; }

        protected override void OnInitialized()
        {
            LoginCustomer = new();
            _httpClient = HttpClientFactory.CreateClient("MyAPI");
        }

        private string message = string.Empty;

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

                await customerData.SaveLoginDataToState(AppState);
                await _httpClient.SaveTokenToLocalStorage(customerData.Token, LocalStorage);

                await InvokeAsync(() =>
                {
                    AppState.IsLoggedIn = true;
                    StateHasChanged();
                });
                await Task.Delay(10);
                Navigation.NavigateTo("/", forceLoad: true);
            }
            else
            {
                message = "Fel användarnamn eller lösenord";
            }
        }
    }
}
