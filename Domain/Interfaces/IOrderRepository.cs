using Domain.Entities;
using Presentation.DTOs;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order, int customerId);
    }
}
