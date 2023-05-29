using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/orderItems")]
[ApiController]
public class OrderItemController : Controller
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItemViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemViewModel>> GetOrderItemById([FromRoute] int id)
    {
        OrderItemViewModel? orderItem;
        try
        {
            orderItem = await _orderItemService.GetById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(orderItem);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddOrderItem([FromBody] OrderItemInputModel orderItemInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _orderItemService.Add(orderItemInputModel);
        return CreatedAtAction(nameof(GetOrderItemById), new { id }, id);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderInput([FromRoute] int id, [FromBody] OrderItemInputModel orderItemInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _orderItemService.Update(id, orderItemInputModel);
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
    public async Task<ActionResult> DeleteOrderItem([FromRoute] int id)
    {
        try
        {
            await _orderItemService.Delete(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}