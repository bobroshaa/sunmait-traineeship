using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
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

    [HttpPost]
    public async Task<ActionResult<int>> AddOrderItem([FromBody] OrderItemInputModel orderItemInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _orderItemService.Add(orderItemInputModel));
    }

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