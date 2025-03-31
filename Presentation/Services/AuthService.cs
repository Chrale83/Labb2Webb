using Presentation.DTOs;
using Presentation.Interfaces;

namespace Presentation.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private const string authUri = "api/auth";

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        public async Task RegisterCustomerAsync(CustomerDTO customerRegisterDto)
        {
            await httpClient.PostAsJsonAsync($"{authUri}/register", customerRegisterDto);
        }

        Task<CustomerLoginDTO> IAuthService.LoginAsync(CustomerLoginDTO customerLoginDto)
        {
            throw new NotImplementedException();
        }
    }
}
