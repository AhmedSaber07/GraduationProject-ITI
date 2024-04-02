using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.ReviewDto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public readonly ireviewService _reviewService;
        public ReviewController(ireviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("{productId:guid}")]
        public async Task<ActionResult<IEnumerable<GetReviewDto>>> Reviews(Guid productId)
        {
            return Ok(await _reviewService.GetProductReviews(productId));
        }

        [HttpPost]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> AddReview([FromBody] CreateOrUpdateDto reviewDto)
        {
            if (ModelState.IsValid)
            {
                var resultreview = await _reviewService.CreateReview(reviewDto);
                return Created("Review", resultreview);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> UpdateReview(Guid reviewId, [FromBody] CreateOrUpdateDto reviewDto)
        {
            if (await _reviewService.ReviewExist(reviewId))
            {
                if (ModelState.IsValid)
                {
                    var resultreview = await _reviewService.UpdateReview(reviewDto, reviewId);
                    return Created("Review", resultreview);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult<resultDto<GetReviewDto>>> RemoveReview(Guid reviewId)
        {
            if (reviewId != Guid.Empty)
            {
                var review = await _reviewService.softDeleteReview(reviewId);
                if (review is not null)
                {
                    return Ok(review);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
    }
}
