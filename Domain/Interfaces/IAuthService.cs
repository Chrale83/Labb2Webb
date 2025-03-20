using Domain.Dtos;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Customer?> RegisterAsync(CustomerRegisterDto request);

        //Task<string?> LoginAsync(CustomerLoginDto request);
        Task<CustomerLoginResponseDto> LoginAsync(CustomerLoginDto request);
    }
}
