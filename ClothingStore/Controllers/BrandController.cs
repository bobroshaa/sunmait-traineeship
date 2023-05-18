using AutoMapper;
using ClothingStore.Entities;
using ClothingStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : Controller
{
    private readonly Context _dbContext;
    private readonly IMapper _mapper;

    public BrandController(Context dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<BrandViewModel>>> GetAllBrands()
    {
        var brands = _mapper.Map<List<BrandViewModel>>(await _dbContext.Brands.Where(b => b.IsActive).ToListAsync());
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandViewModel>> GetBrand(int id)
    {
        var brand = _mapper.Map<BrandViewModel>(await _dbContext.Brands.FirstOrDefaultAsync(b => b.ID == id & b.IsActive));
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddBrand(BrandInputModel brandInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var brand = _mapper.Map<BrandInputModel, Brand>(brandInputModel); 
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();
        return Ok(brand.ID);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBrand(BrandViewModel brandViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.ID == brandViewModel.ID & b.IsActive);
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }
        
        brand.Name = brandViewModel.Name;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBrand(int id)
    {
        var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.ID == id & b.IsActive);
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        brand.IsActive = false;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}