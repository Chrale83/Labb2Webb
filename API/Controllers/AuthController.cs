using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register(CustomerDto request)
        {
            var customer = await _authService.RegisterAsync(request);
            if (customer == null)
                return BadRequest("Customer already exists!");

            return Ok(customer);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(CustomerLoginDto request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("invalid username or password");
            }

            return Ok(token);
        }
    }
}
