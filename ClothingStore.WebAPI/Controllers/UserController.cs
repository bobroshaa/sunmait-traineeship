using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserViewModel>))]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpGet]
    public async Task<ActionResult<List<UserViewModel>>> GetAllUsers()
    {
        var users = await _userService.GetAll();
        
        return Ok(users);
    }

    /// <summary>
    /// Get a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserViewModel>> GetUser([FromRoute] int id)
    {
        var user = await _userService.GetById(id);
        
        return Ok(user);
    }

    /// <summary>
    /// Add a new user.
    /// </summary>
    /// <param name="userInputModel">The input model of the new user.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpPost]
    public async Task<ActionResult<int>> AddUser([FromBody] UserInputModel userInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _userService.Add(userInputModel);
        
        return Ok(response);
    }

    /// <summary>
    /// Update a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="userInputModel">The input model of the user.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserInputModel userInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.Update(id, userInputModel);
        
        return Ok();
    }

    /// <summary>
    /// Delete a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        await _userService.Delete(id);
        
        return Ok();
    }

    /// <summary>
    /// Update user address by him ID.
    /// </summary>
    /// <param name="userId">The ID of the section.</param>
    /// <param name="addressInputModel">The input model of the address.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpPut("{userId}/address")]
    public async Task<ActionResult> UpdateAddress([FromRoute] int userId,
        [FromBody] AddressInputModel addressInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.UpdateAddress(userId, addressInputModel);
        
        return Ok();
    }

    /// <summary>
    /// Update user role by ID.
    /// </summary>
    /// <param name="userId">The ID of the section.</param>
    /// <param name="role">A new role of the user.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpPut("{userId}/role/{role}")]
    public async Task<ActionResult> UpdateRole([FromRoute] int userId, [FromRoute] Role role)
    {
        await _userService.UpdateRole(userId, role);
        
        return Ok();
    }
}