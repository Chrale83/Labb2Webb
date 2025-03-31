using Presentation.DTOs;

namespace Presentation.Interfaces
{
    public interface IAuthService
    {
        Task RegisterCustomerAsync(CustomerDTO customerRegisterDto);
        Task<CustomerLoginDTO> LoginAsync(CustomerLoginDTO customerLoginDto);
    }
}
