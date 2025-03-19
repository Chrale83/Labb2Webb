using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;

            _authRepository = authRepository;
        }

        public async Task<string?> LoginAsync(CustomerLoginDto request)
        {
            var customer = await _authRepository.LoginAsync(request.Email);

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
            var response = await _authRepository.CheckIfRegistred(request);

            if (response)
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
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.StreetName = request.StreetName;

            return await _authRepository.RegisterAsync(customer);
        }

        private string CreateToken(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Role, customer.Role.ToString()),
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
