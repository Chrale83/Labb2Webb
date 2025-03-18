using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = appDbContext;
        }

        public async Task<string?> LoginAsync(CustomerLoginDto request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c =>
                c.Email == request.Email
            );

            if (customer == null)
            {
                return null;
            }
            if (
                new PasswordHasher<Customer>().VerifyHashedPassword(
                    customer,
                    customer.PasswordHash,
                    request.Password
                ) == PasswordVerificationResult.Failed
            )
            {
                return null;
            }

            return CreateToken(customer);
        }

        public async Task<Customer?> RegisterAsync(CustomerDto request)
        {
            if (await _context.Customers.AnyAsync(c => c.Email == request.Email))
            {
                return null;
            }

            var customer = new Customer();

            var hashedPassword = new PasswordHasher<Customer>().HashPassword(
                customer,
                request.Password
            );

            customer.Email = request.Email;
            customer.PasswordHash = hashedPassword;
            customer.PhoneNumber = request.PhoneNumber;
            customer.StreetNumber = request.StreetNumber;
            customer.City = request.City;
            customer.FirstName = request.LastName;
            customer.LastName = request.LastName;
            customer.StreetName = request.StreetName;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private string CreateToken(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
