using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace ClothingStore.WebAPI;

public class JwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(UserViewModel user)
    {
        var key = _configuration["JwtConfig:Secret"];
        var secret = Encoding.UTF8.GetBytes(key);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new []
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email)
                }
            ),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256),
            Audience = _configuration["JwtConfig:Audience"],
            Issuer = _configuration["JwtConfig:Issuer"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}