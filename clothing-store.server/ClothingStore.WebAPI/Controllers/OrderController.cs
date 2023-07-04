using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.WebAPI.Hubs;
using ClothingStore.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IHubContext<ProductHub> _productHub;
    private readonly ISignalRService _signalRService;

    public OrderController(IOrderService orderService, IHubContext<ProductHub> productHub,
        ISignalRService signalRService)
    {
        _orderService = orderService;
        _productHub = productHub;
        _signalRService = signalRService;
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderViewModel>))]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpGet]
    public async Task<ActionResult<List<OrderViewModel>>> GetAllOrders()
    {
        var orders = await _orderService.GetAll();

        return Ok(orders);
    }

    /// <summary>
    /// Get order items by order ID.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderItemViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("{orderId}/items")]
    public async Task<ActionResult<List<OrderItemViewModel>>> GetOrderItemsByOrder(int orderId)
    {
        var orderItems = await _orderService.GetOrderItemsByOrderId(orderId);

        return Ok(orderItems);
    }

    /// <summary>
    /// Get an order by ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderViewModel>> GetOrder([FromRoute] int id)
    {
        var order = await _orderService.GetById(id);

        return Ok(order);
    }


    // Todo: return orderId
    /// <summary>
    /// Add a new order with items.
    /// </summary>
    /// <param name="orderInputModel">The input model of the new order.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpPost]
    public async Task<ActionResult<int>> AddOrder([FromBody] OrderInputModel orderInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var orderId = await _orderService.Add(orderInputModel);

        var cartItems = orderInputModel.CartItems.ToDictionary(item => item.ProductID, item => item);

        if (!cartItems.IsNullOrEmpty())
        {
            foreach (var item in cartItems)
            {
                await _signalRService.UpdateReservedQuantity(
                    item.Key,
                    item.Value.Product.ReservedQuantity - item.Value.Quantity);
                await _signalRService.UpdateInStockQuantity(
                    item.Key,
                    item.Value.Product.InStockQuantity - item.Value.Quantity);
            }

            await _signalRService.UpdateCart(orderInputModel.CartItems[0].UserID);
        }

        return Ok(orderId);
    }

    /// <summary>
    /// Update order status by order ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="orderStatus">A new status of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpPut("{id}/status/{status}")]
    public async Task<ActionResult> UpdateOrderStatus([FromRoute] int id, [FromRoute] Status status)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _orderService.Update(id, status);

        return Ok();
    }

    /// <summary>
    /// Delete an order by ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] int id)
    {
        var products = await _orderService.Delete(id);

        if (!products.IsNullOrEmpty())
        {
            foreach (var product in products)
            {
                await _signalRService.UpdateReservedQuantity(product.ID, product.ReservedQuantity);
                await _signalRService.UpdateInStockQuantity(product.ID, product.InStockQuantity);
            }
        }

        return Ok();
    }

    /// <summary>
    /// Get order history order by ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("{id}/history")]
    public async Task<ActionResult<List<OrderHistory>>> GetOrderHistory([FromRoute] int id)
    {
        var order = await _orderService.GetOrderHistoryByOrderId(id);

        return Ok(order);
    }
}