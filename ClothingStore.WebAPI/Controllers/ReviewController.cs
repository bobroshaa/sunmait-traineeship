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

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewViewModel>> GetReview([FromRoute] int id)
    {
        var review = await _reviewService.GetById(id);
        return Ok(review);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReviewViewModel>))]
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<List<ReviewViewModel>>> GetReviewsByProductId([FromRoute] int productId)
    {
        var reviews = await _reviewService.GetReviewsByProductId(productId);
        return Ok(reviews);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> AddReview([FromBody] ReviewInputModel reviewInputModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _reviewService.Add(reviewInputModel);
        return CreatedAtAction(nameof(GetReview), new { id }, id);
    }

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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReview([FromRoute] int id)
    {
        await _reviewService.Delete(id);
        return Ok();
    }
}