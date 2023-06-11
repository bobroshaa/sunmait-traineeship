using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    private readonly IProductService _productService;

    public BrandController(IBrandService brandService, IProductService productService)
    {
        _brandService = brandService;
        _productService = productService;
    }
    
    /// <summary>
    /// Get all brands.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BrandViewModel>))]
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<BrandViewModel>>> GetAllBrands()
    {
        var brands = await _brandService.GetAll();
        
        return Ok(brands);
    }
    
    /// <summary>
    /// Get a brand by ID.
    /// </summary>
    /// <returns>The brand view model.</returns>
    /// <param name="id">The ID of the brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandViewModel>> GetBrand([FromRoute] int id)
    {
        var brand = await _brandService.GetById(id);
        
        return Ok(brand);
    }

    /// <summary>
    /// Add a new brand.
    /// </summary>
    /// <param name="brandInputModel">The input model of the new brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPost]
    public async Task<ActionResult<int>> AddBrand([FromBody] BrandInputModel brandInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _brandService.Add(brandInputModel);
        
        return Ok(response);
    }

    /// <summary>
    /// Update a brand by ID.
    /// </summary>
    /// <param name="id">The ID of the brand.</param>
    /// <param name="brandInputModel">The input model of the brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBrand([FromRoute] int id, [FromBody] BrandInputModel brandInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _brandService.Update(id, brandInputModel);
        
        return Ok();
    }

    /// <summary>
    /// Delete a brand by ID.
    /// </summary>
    /// <param name="id">The ID of the brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBrand([FromRoute] int id)
    {
        await _brandService.Delete(id);
        
        return Ok();
    }

    /// <summary>
    /// Assign a product to a brand. 
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="brandId">The ID of the brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPut("{brandId}/products/{productId}/assign")]
    public async Task<ActionResult> AssignToBrand([FromRoute] int productId, [FromRoute] int brandId)
    {
        await _productService.AssignToBrand(productId, brandId);
        
        return Ok();
    }

    /// <summary>
    /// Unassign a product from a brand. 
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPut("products/{productId}/unassign")]
    public async Task<ActionResult> UnassignFromBrand([FromRoute] int productId)
    {
        await _productService.UnassignFromBrand(productId);
        
        return Ok();
    }
}