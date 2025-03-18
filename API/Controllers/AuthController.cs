using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
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

            string token = CreateToken(customer);

            return Ok(token);
        }

        private string CreateToken(Customer customer)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, customer.Email) };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience "),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
