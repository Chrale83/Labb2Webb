using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<Customer>> Register(CustomerRegisterDto request)
        {
            var customer = await _authService.RegisterAsync(request);
            if (customer == null)
                return BadRequest("Customer already exists!");

            return Ok(customer);
        }

        [HttpPost("login")]
        public async Task<ActionResult<CustomerLoginResponseDto>> Login(CustomerLoginDto request)
        {
            var response = await _authService.LoginAsync(request);
            if (response == null)
            {
                return BadRequest("invalid username or password");
            }

            return Ok(response);
        }

        //[Authorize]
        //[HttpGet]
        //public IActionResult AuthenticatedOnlyEndpoint()
        //{
        //    return Ok("you are autentitcated");
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpGet("admin-only")]
        //public IActionResult AdminOnlyEndpoint()
        //{
        //    return Ok("Du har admin rättigheter");
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpGet("user-only")]
        //public IActionResult UserOnlyEndpoint()
        //{
        //    return Ok("du har user rättigheter");
        //}
    }
}
