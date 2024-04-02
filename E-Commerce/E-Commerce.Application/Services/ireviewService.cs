using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.ReviewDto;

namespace E_Commerce.Application.Services
{
    public interface ireviewService
    {
        Task<resultDto<CreateOrUpdateDto>> CreateReview(CreateOrUpdateDto reviewDto);
        Task<resultDto<CreateOrUpdateDto>> UpdateReview(CreateOrUpdateDto EditedreviewDto, Guid reviewId);
        Task<resultDto<GetReviewDto>> HardDeleteReview(Guid reviewId);
        Task<resultDto<GetReviewDto>> softDeleteReview(Guid reviewId);
        Task<IEnumerable<GetReviewDto>> GetProductReviews(Guid productId);
        Task<bool> ReviewExist(Guid reviewId);
    }
}
