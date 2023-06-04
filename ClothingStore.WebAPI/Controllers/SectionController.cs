using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/sections")]
[ApiController]
public class SectionController : Controller
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    /// <summary>
    /// Get all sections.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SectionViewModel>))]
    [HttpGet]
    public async Task<ActionResult<List<SectionViewModel>>> GetAllSections()
    {
        var sections = await _sectionService.GetAll();
        
        return Ok(sections);
    }

    /// <summary>
    /// Get a section by ID.
    /// </summary>
    /// <param name="id">The ID of the section.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SectionViewModel>> GetSection([FromRoute] int id)
    {
        var section = await _sectionService.GetById(id);
        
        return Ok(section);
    }

    /// <summary>
    /// Add a new section.
    /// </summary>
    /// <param name="sectionInputModel">The input model of the new section.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddSection([FromBody] SectionInputModel sectionInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _sectionService.Add(sectionInputModel);
        
        return Ok(response);
    }

    /// <summary>
    /// Update section name by section ID.
    /// </summary>
    /// <param name="id">The ID of the section.</param>
    /// <param name="sectionInputModel">The input model of the section.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSectionName([FromRoute] int id,
        [FromBody] SectionInputModel sectionInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _sectionService.Update(id, sectionInputModel);
        
        return Ok();
    }
    
    /// <summary>
    /// Delete a section by ID.
    /// </summary>
    /// <param name="id">The ID of the section.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSection([FromRoute] int id)
    {
        await _sectionService.Delete(id);
        
        return Ok();
    }
}