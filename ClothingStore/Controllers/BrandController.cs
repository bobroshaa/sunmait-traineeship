using ClothingStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : Controller
{
    private readonly Context _dbContext;

    public BrandController(Context dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Brand>>> GetAllBrands()
    {
        var brands = await _dbContext.Brands.ToListAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(int id)
    {
        var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.ID == id);
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<List<Brand>>> AddBrand(Brand brand)
    {
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();
        return Ok(await _dbContext.Brands.ToListAsync());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Brand>> UpdateBrand(int id, Brand brand)
    {
        var oldBrand = await _dbContext.Brands.FindAsync(id);
        if (oldBrand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        var updatedBrand = _dbContext.Brands.Update(oldBrand);
        await _dbContext.SaveChangesAsync();
        return Ok(updatedBrand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Brand>>> DeleteBrand(int id)
    {
        var brand = await _dbContext.Brands.FindAsync(id);
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        _dbContext.Brands.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return Ok(await _dbContext.Brands.ToListAsync());
    }
}