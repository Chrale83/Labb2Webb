using Presentation.DTOs;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDTO orderDto, int customerId);
    }
}
