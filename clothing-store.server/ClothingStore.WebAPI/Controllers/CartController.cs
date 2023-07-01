using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.WebAPI.Hubs;
using ClothingStore.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ISignalRService _signalRService;

    public CartController(ICartService cartService, ISignalRService signalRService)
    {
        _cartService = cartService;
        _signalRService = signalRService;
    }

    /// <summary>
    /// Get all items contained in the user's cart.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CartItemViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<CartItemViewModel>>> GetUserCart([FromRoute] int userId)
    {
        var cartItems = await _cartService.GetUserCart(userId);

        return Ok(cartItems);
    }

    /// <summary>
    /// Get a cart item by ID.
    /// </summary>
    /// <param name="id">The ID of the cart item.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("items/{id}")]
    public async Task<ActionResult<CartItemViewModel>> GetCartItem([FromRoute] int id)
    {
        var cartItem = await _cartService.GetById(id);

        return Ok(cartItem);
    }

    /// <summary>
    /// Add a new cart item.
    /// </summary>
    /// <param name="cartItemInputModel">The input model of the new cart item.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpPost]
    public async Task<ActionResult<int>> AddCartItem([FromBody] CartItemInputModel cartItemInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newCartItem = await _cartService.Add(cartItemInputModel);

        await _signalRService.UpdateReservedQuantity(newCartItem.ProductID, newCartItem.ReservedQuantity);
        await _signalRService.UpdateCart(newCartItem.UserID);

        return Ok(newCartItem);
    }

    /// <summary>
    /// Update a number of the cart item by ID.
    /// </summary>
    /// <param name="id">The ID of the cart item.</param>
    /// <param name="count">A new number of the cart item.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCartItem([FromRoute] int id, [FromBody] int count)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cartItem = await _cartService.Update(id, count);
        
        await _signalRService.UpdateReservedQuantity(cartItem.ProductID, cartItem.ReservedQuantity);
        await _signalRService.UpdateCartItemQuantity(cartItem.UserID, cartItem.ID, cartItem.Quantity);

        return Ok(cartItem);
    }

    /// <summary>
    /// Delete a cart item by ID.
    /// </summary>
    /// <param name="id">The ID of the cart item.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCartItem([FromRoute] int id)
    {
        var cartItem = await _cartService.Delete(id);

        await _signalRService.UpdateReservedQuantity(cartItem.ProductID, cartItem.ReservedQuantity);
        await _signalRService.UpdateCart(cartItem.UserID);
        

        return Ok();
    }
}