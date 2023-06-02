using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebAPI.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Get review by ID.
    /// </summary>
    /// <param name="id">The ID of the review.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewViewModel>> GetReview([FromRoute] int id)
    {
        var review = await _reviewService.GetById(id);
        
        return Ok(review);
    }

    /// <summary>
    /// Get reviews by product ID.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReviewViewModel>))]
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<List<ReviewViewModel>>> GetReviewsByProductId([FromRoute] int productId)
    {
        var reviews = await _reviewService.GetReviewsByProductId(productId);
        
        return Ok(reviews);
    }

    /// <summary>
    /// Add a new review.
    /// </summary>
    /// <param name="reviewInputModel">The input model of the new review.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddReview([FromBody] ReviewInputModel reviewInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _reviewService.Add(reviewInputModel);
        
        return Ok(id);
    }

    /// <summary>
    /// Update a review by ID.
    /// </summary>
    /// <param name="id">The ID of the review.</param>
    /// <param name="reviewInputModel">The input model of the review.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateReview([FromRoute] int id, [FromBody] ReviewInputModel reviewInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _reviewService.Update(id, reviewInputModel);
        
        return Ok();
    }

    /// <summary>
    /// Delete a review by ID.
    /// </summary>
    /// <param name="id">The ID of the review.</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReview([FromRoute] int id)
    {
        await _reviewService.Delete(id);
        
        return Ok();
    }
}