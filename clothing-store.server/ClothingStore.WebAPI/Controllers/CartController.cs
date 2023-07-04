using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Application.Options;
using ClothingStore.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ISignalRService _signalRService;
    private readonly IScheduleService _scheduleService;
    private readonly ReservationConfiguration _reservationConfiguration;

    public CartController(
        ICartService cartService,
        ISignalRService signalRService,
        IScheduleService scheduleService,
        IOptions<ReservationConfiguration> options)
    {
        _cartService = cartService;
        _signalRService = signalRService;
        _scheduleService = scheduleService;
        _reservationConfiguration = options.Value;
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

        _scheduleService.Schedule(newCartItem.ID, TimeSpan.FromSeconds(_reservationConfiguration.reservationTime));

        await _signalRService.UpdateReservedQuantity(newCartItem.ProductID, newCartItem.Product.ReservedQuantity);
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
    public async Task<ActionResult> UpdateCartItemQuantity([FromRoute] int id, [FromBody] int count)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cartItem = await _cartService.UpdateQuantity(id, count);

        await _signalRService.UpdateReservedQuantity(cartItem.ProductID, cartItem.Product.ReservedQuantity);
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

        await _signalRService.UpdateReservedQuantity(cartItem.ProductID, cartItem.Product.ReservedQuantity);
        await _signalRService.UpdateCart(cartItem.UserID);

        return Ok();
    }
}