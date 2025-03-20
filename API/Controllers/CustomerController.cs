using System.Security.Claims;
using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerProfileDto>> GetCustomer(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userIdFromToken = int.Parse(userIdClaim);

            if (userIdFromToken == id)
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer != null)
                {
                    return Ok(customer);
                }
                return NotFound("kunden hittades inte");
            }
            return Forbid("du har ingen behörighet");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerEditDto customerEditDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userIdFromToken = int.Parse(userIdClaim);

            if (userIdFromToken == customerEditDto.Id)
            {
                var response = await _customerService.UpdateCustomerAsync(customerEditDto);
                if (response)
                {
                    return Ok();
                }
                return NotFound();
            }
            return Forbid("du har ingen behörighet till detta");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            return Ok();
        }
    }
}
