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
        List<OrderItemViewModel> orderItems;
        try
        {
            orderItems = await _orderService.GetOrderItemsByOrderId(orderId);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(orderItems);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderViewModel>> GetOrder([FromRoute] int id)
    {
        OrderViewModel? order;
        try
        {
            order = await _orderService.GetById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

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

        try
        {
            var id = await _orderService.Add(orderInputModel);
            return CreatedAtAction(nameof(GetOrder), new { id }, id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

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

        try
        {
            await _orderService.Update(id, orderStatus);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] int id)
    {
        try
        {
            await _orderService.Delete(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{orderId}/items")]
    public async Task<ActionResult<int>> AddOrderItemInOrder([FromRoute] int orderId,
        [FromBody] OrderItemInputModel orderItemInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var id = await _orderService.AddOrderItemInOrder(orderId, orderItemInputModel);
            return CreatedAtAction(nameof(GetOrder), new { id }, id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("items/{orderItemId}")]
    public async Task<ActionResult> DeleteOrderItemFromOrder([FromRoute] int orderItemId)
    {
        try
        {
            await _orderService.DeleteOrderItemFromOrder(orderItemId);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}