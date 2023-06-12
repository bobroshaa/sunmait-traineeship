using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IUserService _userService;
    private readonly JwtGenerator _jwtGenerator;

    public AuthenticationController(IUserService userService, JwtGenerator jwtGenerator)
    {
        _userService = userService;
        _jwtGenerator = jwtGenerator;
    }
    
    /// <summary>
    /// Authenticate user.
    /// </summary>
    /// <param name="loginInputModel">Input model with login data.</param>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Authenticate([FromBody]LoginInputModel loginInputModel)
    {
        var user = await _userService.Authenticate(loginInputModel);

        var token = _jwtGenerator.CreateToken(user);
        
        return Ok(token);
    }
}