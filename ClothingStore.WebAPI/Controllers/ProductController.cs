using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductViewModel>>> GetAllProducts()
    {
        var products = await _productService.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> GetProduct([FromRoute] int id)
    {
        ProductViewModel? product;
        try
        {
            product = await _productService.GetById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddProduct([FromBody] ProductInputModel productInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _productService.Add(productInputModel));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductInputModel productInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _productService.Update(id, productInputModel);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        try
        {
            await _productService.Delete(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}