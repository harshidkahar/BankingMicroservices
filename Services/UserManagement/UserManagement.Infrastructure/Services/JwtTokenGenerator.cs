using UserManagement.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserManagement.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{    
    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("YourSuperSecureKeyForJWTGeneration1234")),
                    SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        var sercurityToken = new JwtSecurityToken(
            issuer: "DemoIssuer",
            audience: "DemoAudience",
            expires: DateTime.UtcNow.AddMinutes(60), //_dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return (new JwtSecurityTokenHandler().WriteToken(sercurityToken));
    }
}
