using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryViewModel>> GetCategory([FromRoute] int id)
    {
        var category = await _categoryService.GetById(id);
        
        return Ok(category);
    }

    /// <summary>
    /// Add a new category.
    /// </summary>
    /// <param name="categoryInputModel">The input model of the new category.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPost]
    public async Task<ActionResult<int>> AddCategory([FromBody] CategoryInputModel categoryInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _categoryService.Add(categoryInputModel);
        
        return Ok(response);
    }

    /// <summary>
    /// Update a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <param name="categoryInputModel">The input model of the category.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryInputModel categoryInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _categoryService.Update(id, categoryInputModel);
        
        return Ok();
    }

    /// <summary>
    /// Delete a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory([FromRoute] int id)
    {
        await _categoryService.Delete(id);
        
        return Ok();
    }
    
    /// <summary>
    /// Link a category to the section.
    /// </summary>
    /// <param name="categoryId">The ID of the category.</param>
    /// <param name="sectionId">The ID of the section.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPost("{categoryId}/sections/{sectionId}/link")]
    public async Task<ActionResult<int>> LinkCategoryToSection([FromRoute] int sectionId, [FromRoute] int categoryId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _categoryService.LinkCategoryToSection(sectionId, categoryId);
        
        return Ok();
    }
}