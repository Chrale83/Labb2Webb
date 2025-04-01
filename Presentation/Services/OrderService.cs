using Blazored.LocalStorage;
using Presentation.DTOs;
using Presentation.Extensions;
using Presentation.Interfaces;

namespace Presentation.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private const string orderUri = "api/orders";

        public OrderService(
            IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService
        )
        {
            localStorage = localStorageService;
            httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        public async Task<bool> CreateOrder(OrderDTO newOrder)
        {
            await httpClient.SetTokenToHttpClientFromLStorage(localStorage);
            var response = await httpClient.PostAsJsonAsync($"{orderUri}", newOrder);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<OrderForCustomerDTO>> GetOrdersForCustomer(int customerId)
        {
            return await httpClient.GetFromJsonAsync<List<OrderForCustomerDTO>>(
                $"{orderUri}/{customerId}"
            );
        }
    }
}
