using Domain.Dtos;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Customer> RegisterAsync(Customer customer);
        Task<bool> CheckIfRegistred(CustomerRegisterDto request);
        Task<Customer?> LoginAsync(string email);
    }
}
