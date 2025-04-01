using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderDTO newOrder);

        Task<OrderForCustomerDTO> GetOrdersForCustomer(int customerId);
    }
}
