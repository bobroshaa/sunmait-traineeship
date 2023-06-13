using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.WebAPI.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ClothingStore.WebAPI.Services;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtConfiguration _jwtConfiguration;

    public JwtGenerator(IOptions<JwtConfiguration> options)
    {
        _jwtConfiguration = options.Value;
    }
    
    public string CreateToken(UserViewModel user)
    {
        var secret = Encoding.UTF8.GetBytes(_jwtConfiguration.Secret);

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
            Audience = _jwtConfiguration.Audience,
            Issuer = _jwtConfiguration.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}