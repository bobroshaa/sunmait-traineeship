using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
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

    [HttpGet]
    public async Task<ActionResult<List<OrderViewModel>>> GetAllOrders()
    {
        var orders = await _orderService.GetAll();
        return Ok(orders);
    }
    
    [HttpGet("{orderId}/items")]
    public async Task<ActionResult<List<OrderViewModel>>> GetOrderItemsByOrder(int orderId)
    {
        var orderItems = await _orderService.GetAllByOrderId(orderId);
        return Ok(orderItems);
    }

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

    [HttpPost]
    public async Task<ActionResult<int>> AddOrder([FromBody] OrderInputModel orderInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _orderService.Add(orderInputModel));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder([FromRoute] int id, [FromBody] OrderInputModel orderInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _orderService.Update(id, orderInputModel);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }

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
}