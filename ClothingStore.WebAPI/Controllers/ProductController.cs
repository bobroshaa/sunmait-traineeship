using ClothingStore.Application.Interfaces;
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

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
    [HttpGet]
    public async Task<ActionResult<List<ProductViewModel>>> GetAllProducts()
    {
        var products = await _productService.GetAll();
        return Ok(products);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddProduct([FromBody] ProductInputModel productInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _productService.Add(productInputModel);
        return CreatedAtAction(nameof(GetProduct), new { id }, id);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("section-category/{sectionId}/{categoryId}")]
    public async Task<ActionResult<List<ProductViewModel>>> GetProductsBySectionAndCategory([FromRoute] int sectionId,
        [FromRoute] int categoryId)
    {
        try
        {
            var products = await _productService.GetProductsBySectionAndCategory(sectionId, categoryId);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("brand/{brandId}")]
    public async Task<ActionResult<List<ProductViewModel>>> GetProductsByBrand([FromRoute] int brandId)
    {
        try
        {
            var products = await _productService.GetProductsByBrand(brandId);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}