using System.Collections.Generic;
using System.Net;
using Blazored.LocalStorage;
using Presentation.Extensions;
using Presentation.Interfaces;
using Presentation.Models;

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

            //switch (countChar)
            //{
            //    case 0:
            //        return await httpClient.GetFromJsonAsync<List<CustomerProfileModel>>(
            //            "/api/customers"
            //        );

            //    case >= 2:
            //        var response = await httpClient.GetAsync(
            //            $"/api/customers/search?searchWord={customerSearchQuery}"
            //        );

            //        if (response.StatusCode == HttpStatusCode.OK)
            //        {
            //            return await response.Content.ReadFromJsonAsync<
            //                    List<CustomerProfileModel>
            //                >() ?? new();
            //        }
            //        else if (response.StatusCode == HttpStatusCode.NoContent)
            //        {
            //            return new List<CustomerProfileModel>();
            //        }

            //        break;
        }

        public Task<CustomerEditModel> GetCustomerToEdit()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(CustomerEditModel customerEditModel)
        {
            throw new NotImplementedException();
        }
    }
}
