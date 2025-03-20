using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Models;

namespace Presentation.Components.Pages
{
    public partial class RegisterCustomer
    {
        [Inject]
        public IHttpClientFactory httpClientFactory { get; set; } = default!;
        private HttpClient? _httpClient;

        [SupplyParameterFromForm]
        public CustomerNew? Customer { get; set; }
        public CustomerDTO? CustomerDTO { get; set; }

        private async Task RegisterCustomerSubmit()
        {
            CustomerDTO = new CustomerDTO
            {
                FirstName = Customer.FirstName,
                LastName = Customer.LastName,
                Email = Customer.Email,
                Password = Customer.Password,
                City = Customer.City,
                PhoneNumber = Customer.PhoneNumber,
                StreetName = Customer.StreetName,
                StreetNumber = Customer.StreetNumber,
            };

            await _httpClient.PostAsJsonAsync("/api/auth/register", CustomerDTO);
        }

        protected override void OnInitialized()
        {
            Customer ??= new();
            _httpClient = httpClientFactory.CreateClient("MyAPI");
        }
    }
}
