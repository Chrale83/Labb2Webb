using Domain.Dtos;
using Domain.Entities;
using Presentation.DTOs;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDTO orderDto, int customerId);
        Task<List<OrderForCustomerDto>> GetOrdersForCustomer(int customerId);
    }
}
