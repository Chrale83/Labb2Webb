using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;
using Presentation.DTOs;
using Presentation.States;

namespace Presentation.Extensions
{
    public static class ExtensionMethods
    {
        public static Dictionary<string, object> GetUpdatedFields<T>(this T original, T updated)
        {
            var updatedData = new Dictionary<string, object>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var originalValue = prop.GetValue(original);
                var updatedValue = prop.GetValue(updated);

                if (prop.Name == "Id" && prop.PropertyType == typeof(int))
                {
                    updatedData[prop.Name] = null;
                    continue;
                }

                if (!Equals(originalValue, updatedValue) && updatedValue != null)
                {
                    updatedData[prop.Name] = updatedValue;
                }
            }
            return updatedData;
        }

        public static async Task SaveLoginDataToState(
            this CustomerLoginResponseDTO loginData,
            AppState appState
        )
        {
            appState.IsLoggedIn = true;
            appState.UserInfo.UserId = loginData.UserId;
            appState.UserInfo.Role = loginData.Role;
            appState.UserInfo.Email = loginData.Email;
            appState.UserInfo.FirstName = loginData.FirstName;
            await Task.Delay(10);
        }

        public static async Task SetTokenToHttpClientFromLStorage(
            this HttpClient httpClient,
            ILocalStorageService localStorage
        )
        {
            if (httpClient.DefaultRequestHeaders.Authorization != null)
            {
                return; // Token finns redan, hämta inte igen
            }

            var token = await localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );
            }
        }

        public static void ClearUserData(this AppState data)
        {
            data.UserInfo = new();
        }

        public static async Task SaveTokenToLocalStorage(
            this HttpClient httpClient,
            string token,
            ILocalStorageService localStorage
        )
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Warning: Trying to save an empty or null token.");
                return;
            }

            await localStorage.SetItemAsync("authToken", token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }
    }
}
