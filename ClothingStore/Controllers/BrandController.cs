using Application.Interfaces;
using AutoMapper;
using ClothingStore.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBrandService _brandService;

    public BrandController(IMapper mapper, IBrandService brandService)
    {
        _mapper = mapper;
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BrandViewModel>>> GetAllBrands()
    {
        var brands = _mapper.Map<List<BrandViewModel>>(await _brandService.GetAll());
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandViewModel>> GetBrand([FromRoute]int id)
    {
        var brand = _mapper.Map<BrandViewModel>(await _brandService.GetById(id));
        if (brand is null)
        {
            return NotFound("Sorry, this brand does not exist!");
        }

        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddBrand([FromBody]BrandInputModel brandInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var brand = _mapper.Map<BrandInputModel, Brand>(brandInputModel); 
        if (!await _brandService.Add(brand))
            return BadRequest();
        return Ok(brand.ID);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBrand([FromRoute]int id, [FromBody]BrandInputModel brandInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _brandService.Update(id, _mapper.Map<BrandInputModel, Brand>(brandInputModel));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBrand([FromRoute]int id)
    {
        await _brandService.Delete(id);
        return Ok();
    }
}