using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Customer customer = new();

        [HttpPost("register")]
        public ActionResult<Customer> Register(CustomerDto request)
        {
            var hashedPassword = new PasswordHasher<Customer>().HashPassword(
                customer,
                request.Password
            );

            customer.Email = request.Email;
            customer.PasswordHash = hashedPassword;

            return Ok(customer);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(CustomerDto request)
        {
            if (customer.Email != request.Email)
            {
                return BadRequest("User not found");
            }
            if (
                new PasswordHasher<Customer>().VerifyHashedPassword(
                    customer,
                    customer.PasswordHash,
                    request.Password
                ) == PasswordVerificationResult.Failed
            )
            {
                return BadRequest("Wrong password");
            }

            string token = "success";

            return Ok(token);
        }

        //private string CreateToken(Customer customer)
        //{
        //    var
        //}
    }
}
