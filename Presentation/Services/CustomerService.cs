using System.Collections.Generic;
using System.Net;
using Blazored.LocalStorage;
using Presentation.Extensions;
using Presentation.Interfaces;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;
        private readonly string customerUri = "/api/customers";
        private readonly ILocalStorageService localStorage;

        public CustomerService(
            IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService
        )
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
            localStorage = localStorageService;
        }

        public async Task<List<CustomerProfileModel>> GetAllCustomers()
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            return await httpClient.GetFromJsonAsync<List<CustomerProfileModel>>($"{customerUri}");
        }

        public async Task<List<CustomerProfileModel>> GetCustomerFromSearch(
            string customerSearchQuery,
            bool listHaveCustomers
        )
        {
            var countChar = customerSearchQuery.Length;

            if (countChar >= 2)
            {
                var response = await httpClient.GetAsync(
                    $"{customerUri}/search?searchWord={customerSearchQuery}"
                );

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<CustomerProfileModel>>()
                        ?? new();
                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return new List<CustomerProfileModel>();
                }
            }

            return await httpClient.GetFromJsonAsync<List<CustomerProfileModel>>($"{customerUri}");
        }

        public async Task<CustomerEditModel> GetCustomerToEdit(int userId)
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            var tempObject = await httpClient.GetFromJsonAsync<CustomerEditModel>(
                $"{customerUri}/{userId}"
            );
            return tempObject;
        }

        public async Task UpdateCustomer(CustomerEditModel customerEditModel)
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            await httpClient.PutAsJsonAsync($"{customerUri}", customerEditModel);
        }
    }
}
