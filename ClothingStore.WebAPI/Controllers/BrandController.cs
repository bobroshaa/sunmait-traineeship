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

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BrandViewModel>))]
    [HttpGet]
    public async Task<ActionResult<List<BrandViewModel>>> GetAllBrands()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandViewModel>> GetBrand([FromRoute] int id)
    {
        BrandViewModel? brand;
        try
        {
            brand = await _brandService.GetById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(brand);
    }

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

        try
        {
            await _brandService.Update(id, brandInputModel);
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
    public async Task<ActionResult> DeleteBrand([FromRoute] int id)
    {
        try
        {
            await _brandService.Delete(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}