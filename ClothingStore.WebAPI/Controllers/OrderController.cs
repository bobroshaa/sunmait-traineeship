using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;
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

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderViewModel>))]
    [HttpGet]
    public async Task<ActionResult<List<OrderViewModel>>> GetAllOrders()
    {
        var orders = await _orderService.GetAll();
        return Ok(orders);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderItemViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{orderId}/items")]
    public async Task<ActionResult<List<OrderItemViewModel>>> GetOrderItemsByOrder(int orderId)
    {
        var orderItems = await _orderService.GetOrderItemsByOrderId(orderId);
        return Ok(orderItems);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderViewModel>> GetOrder([FromRoute] int id)
    {
        var order = await _orderService.GetById(id);
        return Ok(order);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddOrder([FromBody] OrderInputModel orderInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _orderService.Add(orderInputModel);
        return CreatedAtAction(nameof(GetOrder), new { id }, id);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderStatus([FromRoute] int id, [FromBody] Status orderStatus)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _orderService.Update(id, orderStatus);
        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] int id)
    {
        await _orderService.Delete(id);
        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{orderId}/items")]
    public async Task<ActionResult<int>> AddOrderItemToOrder([FromRoute] int orderId,
        [FromBody] OrderItemInputModel orderItemInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _orderService.AddOrderItemToOrder(orderId, orderItemInputModel);
        return CreatedAtAction(nameof(GetOrder), new { id }, id);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("items/{orderItemId}")]
    public async Task<ActionResult> DeleteOrderItemFromOrder([FromRoute] int orderItemId)
    {
        await _orderService.DeleteOrderItemFromOrder(orderItemId);
        return Ok();
    }
}