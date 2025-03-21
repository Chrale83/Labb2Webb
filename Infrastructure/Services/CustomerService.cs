using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Extensions;
using Presentation.DTOs;

namespace Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CustomerProfileDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            var DTO = new CustomerProfileDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                City = customer.City,
                StreetName = customer.StreetName,
                StreetNumber = customer.StreetNumber,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };
            return DTO;
        }

        public async Task<IEnumerable<CustomerProfileDto>> GetCustomersAsync()
        {
            return (await _repository.GetAllAsync()).CustomersToDto();
        }

        public Task<IEnumerable<CustomerRegisterDto>> SearchCustomersAsynch(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCustomerAsync(CustomerEditDto customerEditDto)
        {
            var customer = await _repository.GetByIdAsync(customerEditDto.Id);

            customer.PhoneNumber = customerEditDto.PhoneNumber;
            customer.StreetNumber = customerEditDto.StreetNumber;
            customer.FirstName = customerEditDto.FirstName;
            customer.LastName = customerEditDto.LastName;
            customer.City = customerEditDto.City;
            customer.StreetName = customerEditDto.StreetName;

            return await _repository.UpdateAsync(customer);
        }
    }
}
