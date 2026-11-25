using BLL.DTOs.User;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<LoginResponceDTO> CreateTokenAsync(User user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:expiresMinutes"]));
        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Name, user.UserName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:issuer"],
            audience: _configuration["JWT:audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: credential);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var responce = new LoginResponceDTO()
        {
            AccessToken = tokenString,
            ExpiresAt = expiresAt
        };

        return responce;
    }
}
