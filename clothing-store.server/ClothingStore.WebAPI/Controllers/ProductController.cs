using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ISignalRService _signalRService;

    public ProductController(IProductService productService, ISignalRService signalRService)
    {
        _productService = productService;
        _signalRService = signalRService;
    }

    /// <summary>
    /// Get all products.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet]
    public async Task<ActionResult<List<ProductViewModel>>> GetAllProducts()
    {
        var products = await _productService.GetAll();

        return Ok(products);
    }

    /// <summary>
    /// Get a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> GetProduct([FromRoute] int id)
    {
        var product = await _productService.GetById(id);

        return Ok(product);
    }

    /// <summary>
    /// Add a new product.
    /// </summary>
    /// <param name="productInputModel">The input model of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpPost]
    public async Task<ActionResult<int>> AddProduct([FromBody] ProductInputModel productInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _productService.Add(productInputModel);

        return Ok(response);
    }

    /// <summary>
    /// Update a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <param name="productInputModel">The input model of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductInputModel productInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productService.Update(id, productInputModel);

        await _signalRService.UpdateInStockQuantity(id, productInputModel.InStockQuantity);

        return Ok();
    }

    /// <summary>
    /// Delete a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.AdminAccess)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        await _productService.Delete(id);

        return Ok();
    }

    /// <summary>
    /// Get products by section ID and category ID.
    /// </summary>
    /// <param name="sectionId">The ID of the section.</param>
    /// <param name="categoryId">The ID of the category.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("sections/{sectionId}/categories/{categoryId}")]
    public async Task<ActionResult<List<ProductViewModel>>> GetProductsBySectionAndCategory([FromRoute] int sectionId,
        [FromRoute] int categoryId)
    {
        var products = await _productService.GetProductsBySectionAndCategory(sectionId, categoryId);

        return Ok(products);
    }

    /// <summary>
    /// Get products by brand ID.
    /// </summary>
    /// <param name="brandId">The ID of the brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PolicyNames.CustomerAccess)]
    [HttpGet("brands/{brandId}")]
    public async Task<ActionResult<List<ProductViewModel>>> GetProductsByBrand([FromRoute] int brandId)
    {
        var products = await _productService.GetProductsByBrand(brandId);

        return Ok(products);
    }
}