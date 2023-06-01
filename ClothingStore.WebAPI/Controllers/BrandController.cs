using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    private readonly IProductService _productServise;

    public BrandController(IBrandService brandService, IProductService productService)
    {
        _brandService = brandService;
        _productServise = productService;
    }
    
    /// <summary>
    /// Get all brands.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BrandViewModel>))]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddBrand([FromBody] BrandInputModel brandInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _brandService.Add(brandInputModel);
        return CreatedAtAction(nameof(GetBrand), new { id }, id);
    }

    /// <summary>
    /// Update a brand by ID.
    /// </summary>
    /// <param name="id">The ID of the brand.</param>
    /// <param name="brandInputModel">The input model of the brand.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [HttpPut("assign")]
    public async Task<ActionResult> AssignToBrand([FromQuery] int productId, [FromQuery] int brandId)
    {
        await _productServise.AssignToBrand(productId, brandId);
        return Ok();
    }

    /// <summary>
    /// Unassign a product from a brand. 
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("unassign")]
    public async Task<ActionResult> UnassignFromBrand([FromQuery] int productId)
    {
        await _productServise.UnassignFromBrand(productId);
        return Ok();
    }
}