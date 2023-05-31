using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/sections")]
[ApiController]
public class SectionController :Controller
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SectionViewModel>))]
    [HttpGet]
    public async Task<ActionResult<List<SectionViewModel>>> GetAllSections()
    {
        var sections = await _sectionService.GetAll();
        return Ok(sections);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SectionViewModel>> GetSection([FromRoute] int id)
    {
        SectionViewModel? section;
        try
        {
            section = await _sectionService.GetById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(section);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddSection([FromBody] SectionInputModel sectionInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _sectionService.Add(sectionInputModel);
        return CreatedAtAction(nameof(GetSection), new { id }, id);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSectionName([FromRoute] int id, [FromBody] SectionInputModel sectionInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _sectionService.Update(id, sectionInputModel);
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
    public async Task<ActionResult> DeleteSection([FromRoute] int id)
    {
        try
        {
            await _sectionService.Delete(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}