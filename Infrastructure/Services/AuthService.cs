using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        public AuthService() { }

        public Task<string?> LoginAsync(CustomerLoginDto request)
        {
            throw new NotImplementedException();
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

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
