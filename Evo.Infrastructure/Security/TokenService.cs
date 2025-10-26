using Evo.Application.Contracts.Security;
using Evo.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Evo.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(User user, string role)
        {
            var tokenKey = _config["TokenKey"];
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new InvalidOperationException("TokenKey is missing in configuration.");

            if (tokenKey.Length < 64)
                throw new InvalidOperationException("TokenKey must be at least 64 characters long for security reasons.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            // Claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, role), // use the role passed here
                new Claim(JwtRegisteredClaimNames.UniqueName, user.DisplayName)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
