using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Repositories.Interfaces;
using Services.CustomModels.Request;
using Services.CustomModels.Response;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class BranchAccountService : IBranchAccountService
    {
        private readonly IBranchAccountRepository _branchAccountRepository;
        private readonly IConfiguration _configuration;

        public BranchAccountService(IBranchAccountRepository branchAccountRepository, IConfiguration configuration)
        {
            _branchAccountRepository = branchAccountRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> AuthenticateAsync(LoginRequest request)
        {
            try
            {
                var account = await _branchAccountRepository.AuthenticateAsync(request.EmailAddress, request.AccountPassword);
                if (account == null)
                {
                    throw new Exception("Wrong email or password");
                }

                var response = new LoginResponse
                {
                    EmailAddress = account.EmailAddress,
                    FullName = account.FullName,
                    Role = account.Role,
                    Token = GenerateJwtToken(account)
                };
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateJwtToken(BranchAccount user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Map the role based on user role integer value
            var role = user.Role switch
            {
                1 => "Administrator",
                2 => "Staff",
                3 => "Manager",
                4 => "Member",
                _ => "Member" // Default to Member if Role is unrecognized
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, role)
            };

            // Get the secret key from JwtSettings
            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("JWT SecretKey is missing in configuration.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Get issuer, audience, and expiry from configuration
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = double.Parse(jwtSettings["ExpiryMinutes"] ?? "60"); // Default to 60 minutes if null

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
