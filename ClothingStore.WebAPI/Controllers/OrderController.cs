using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderViewModel>))]
    [Authorize(Roles = nameof(Role.Admin))]
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
    [Authorize]
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
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderViewModel>> GetOrder([FromRoute] int id)
    {
        var order = await _orderService.GetById(id);
        
        return Ok(order);
    }

    /// <summary>
    /// Add a new order with items.
    /// </summary>
    /// <param name="orderInputModel">The input model of the new order.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<int>> AddOrder([FromBody] OrderInputModel orderInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _orderService.Add(orderInputModel);
        
        return Ok(response);
    }

    /// <summary>
    /// Update order status by order ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="orderStatus">A new status of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
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
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] int id)
    {
        await _orderService.Delete(id);
        
        return Ok();
    }
    
    /// <summary>
    /// Get order history order by ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpGet("{id}/history")]
    public async Task<ActionResult<List<OrderHistory>>> GetOrderHistory([FromRoute] int id)
    {
        var order = await _orderService.GetOrderHistoryByOrderId(id);
        
        return Ok(order);
    }
}