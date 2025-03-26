using Presentation.Models;

namespace Presentation.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerEditModel> GetCustomerToEdit(int userId);
        Task UpdateCustomer(CustomerEditModel customerEditModel);
        Task<List<CustomerProfileModel>> GetAllCustomers();
        Task<List<CustomerProfileModel>> GetCustomerFromSearch(
            string customerSearchQuery,
            bool listHaveCustomers
        );
    }
}
