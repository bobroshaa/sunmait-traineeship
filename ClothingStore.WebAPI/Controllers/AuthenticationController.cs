using System.Text;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IConfiguration _configuration;
    
    private readonly IUserService _userService;

    public AuthenticationController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }
    
    /// <summary>
    /// Authenticate user.
    /// </summary>
    /// <param name="user">Input model with login data.</param>
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Authenticate([FromBody]LoginInputModel user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
        var token = await _userService.Authenticate(user, key);
        
        if (token == null)
        {
            return Unauthorized();
        }
        
        return Ok(token);
    }
}