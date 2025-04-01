using System.Security.Claims;
using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userIdFromToken = int.Parse(userIdClaim);
            await _orderService.CreateOrderAsync(orderDTO, userIdFromToken);

            return Created();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<OrderForCustomerDto>>> GetCustomerOrders(int id)
        {
            var customerOrders = await _orderService.GetOrdersForCustomer(id);
            if (customerOrders != null && customerOrders.Any())
            {
                return Ok(customerOrders);
            }
            return Ok(new List<OrderForCustomerDto>());
        }
    }
}
