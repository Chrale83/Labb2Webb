using Domain.Dtos;
using Presentation.DTOs;

namespace Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerProfileDto> GetCustomerByIdAsync(int id);

        Task<IEnumerable<CustomerProfileDto>> GetCustomersAsync();

        Task<bool> DeleteCustomerAsync(int id);

        Task<bool> UpdateCustomerAsync(CustomerEditDto customerEditDto);

        Task<IEnumerable<CustomerRegisterDto>> SearchCustomersAsynch(string search);
    }
}
