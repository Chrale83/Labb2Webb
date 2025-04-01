using Presentation.DTOs;

namespace Presentation.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderDTO newOrder);

        Task<List<OrderForCustomerDTO>> GetOrdersForCustomer(int customerId);
    }
}
