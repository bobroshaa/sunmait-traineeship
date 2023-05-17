using AutoMapper;
using ClothingStore.Entities;
using ClothingStore.ViewModels;
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
        var brands = _mapper.Map<List<BrandViewModel>>(await _dbContext.Brands.ToListAsync());
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandViewModel>> GetBrand(int id)
    {
        var brand = _mapper.Map<BrandViewModel>(await _dbContext.Brands.FindAsync(id));
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult> AddBrand(BrandViewModel brandViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var brand = _mapper.Map<BrandViewModel, Brand>(brandViewModel); 
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBrand(BrandViewModel brandViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var oldBrand = _mapper.Map<BrandViewModel>(await _dbContext.Brands.FindAsync(brandViewModel.ID));
        if (oldBrand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }
        
        var brand = _mapper.Map<BrandViewModel, Brand>(brandViewModel); 
        _dbContext.Brands.Update(brand);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBrand(int id)
    {
        var brand = await _dbContext.Brands.FindAsync(id);
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        _dbContext.Brands.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}