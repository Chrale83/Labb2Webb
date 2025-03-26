using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> SearchForCustomer(string searchedText);
    }
}
