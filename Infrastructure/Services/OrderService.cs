using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Presentation.DTOs;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderService() { }

        public async Task CreateOrderAsync(OrderDTO orderDto, int customerId)
        {
            var order = new Order
            {
                CustomerId = customerId,
                PurchaseDate = orderDto.Ordertime,
                TotalPrice = (int)orderDto.TotalCost,
                OrderProducts = orderDto
                    .ProductOrders.Select(po => new OrderProduct
                    {
                        ProductId = po.ProductId,
                        Quantity = po.Quantity,
                    })
                    .ToList(),
            };

            await _orderRepository.CreateOrder(order, customerId);
        }

        public async Task<List<OrderForCustomerDto>> GetOrdersForCustomer(int customerId)
        {
            var orderEntity = await _orderRepository.GetOrdersForCustomer(customerId);

            var customerOrders = new List<OrderForCustomerDto>();

            customerOrders = orderEntity
                .Select(order => new OrderForCustomerDto
                {
                    CustomerFirstName = order.Customer.FirstName,
                    CustomerLastName = order.Customer.LastName,
                    OrderId = order.Id,
                    TotalPrice = order.TotalPrice,
                    PurchaseDate = order.PurchaseDate,
                    Products = order
                        .OrderProducts.Select(op => new OrderProductForCustomerDto
                        {
                            CategoryName = op.Product.Category.Name,
                            ProductName = op.Product.Name,
                            ProductPrice = op.Product.Price,
                            Quantity = op.Quantity,
                        })
                        .ToList(),
                })
                .ToList();
            return customerOrders;
        }
    }
}
