using Domain.Dtos;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Customer?> RegisterAsync(CustomerDto request);
        Task<string?> LoginAsync(CustomerLoginDto request);
    }
}
