using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Settings;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public (string Token, DateTime Expires) GenerateAccessToken(User user)
        {
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
            };

            if (user.UserRoles != null)
            {
                claims.AddRange(
                    user.UserRoles
                        .Where(ur => ur.Role != null)
                        .Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name)));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }

        public (string Token, DateTime Expires) GenerateRefreshToken()
        {
            var expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            return (token, expires);
        }
    }
}
