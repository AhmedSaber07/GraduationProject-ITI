using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.ReviewDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class reviewService : ireviewService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public reviewService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<resultDto<CreateOrUpdateDto>> CreateReview(CreateOrUpdateDto reviewDto)
        {
            CreateOrUpdateDto createdReview = null;

            if (reviewDto is null)
            {
                return new resultDto<CreateOrUpdateDto>() { Entity = createdReview, IsSuccess = false, Message = "Complete all review fields" };
            }
            var reviewEntity = _mapper.Map<Review>(reviewDto);
            reviewEntity.Id = Guid.NewGuid();
            reviewEntity.createdAt = DateTime.Now;
            reviewEntity = await _unit.review.CreateAsync(reviewEntity);
            await _unit.review.SaveChangesAsync();
            createdReview = _mapper.Map<CreateOrUpdateDto>(reviewEntity);
            return new resultDto<CreateOrUpdateDto>() { Entity = createdReview, IsSuccess = true, Message = "review submited" };
        }
        public async Task<resultDto<CreateOrUpdateDto>> UpdateReview(CreateOrUpdateDto EditedreviewDto ,Guid reviewId)
        {
            CreateOrUpdateDto UpdatedReview = null;
            if (EditedreviewDto is null)
            {
                return new resultDto<CreateOrUpdateDto>() { Entity = UpdatedReview, IsSuccess = false, Message = "fields is empty" };
            }
            var reviewEntity = await _unit.review.GetByIdAsync(reviewId);
            reviewEntity.summary=EditedreviewDto.summary;
            reviewEntity.nickName=EditedreviewDto.nickName;
            reviewEntity.reviewText=EditedreviewDto.reviewText;
            reviewEntity.priceRating=EditedreviewDto.priceRating;
            reviewEntity.valueRating=EditedreviewDto.valueRating;
            reviewEntity.qualityRating=EditedreviewDto.qualityRating;
            reviewEntity.updatedAt=DateTime.Now;
            await _unit.review.SaveChangesAsync();

            UpdatedReview = _mapper.Map<CreateOrUpdateDto>(reviewEntity);

            return new resultDto<CreateOrUpdateDto>() { Entity = UpdatedReview, IsSuccess = true, Message = "review Updated successfully" };

        }
        public async Task<resultDto<GetReviewDto>> HardDeleteReview(Guid reviewId)
        {
            GetReviewDto deletedReview = null;
            if (reviewId == Guid.Empty)
            {
                return new resultDto<GetReviewDto>() { Entity = deletedReview, IsSuccess = false, Message = "Id Not Found" };

            }
            var reviewEntity = await _unit.review.GetByIdAsync(reviewId);
            reviewEntity = await _unit.review.HardDeleteAsync(reviewEntity);
            await _unit.product.SaveChangesAsync();
            deletedReview = _mapper.Map<GetReviewDto>(reviewEntity);
            return new resultDto<GetReviewDto>() { Entity = deletedReview, IsSuccess = true, Message = "Deleted Successfully" };
        } 
        public async Task<resultDto<GetReviewDto>> softDeleteReview(Guid reviewId)
        {
            GetReviewDto deletedReview = null;
            if (reviewId == Guid.Empty)
            {
                return new resultDto<GetReviewDto>() { Entity = deletedReview, IsSuccess = false, Message = "Id Not Found" };

            }
            var reviewEntity = await _unit.review.GetByIdAsync(reviewId);
            reviewEntity.IsDeleted = true;
            reviewEntity.deletedAt = DateTime.UtcNow;
            await _unit.product.SaveChangesAsync();
            deletedReview = _mapper.Map<GetReviewDto>(reviewEntity);
            return new resultDto<GetReviewDto>() { Entity = deletedReview, IsSuccess = true, Message = "Deleted Successfully" };
        }
        public async Task<ReviewListDto<GetReviewDto>> GetProductReviews (Guid productId)
        {
            decimal rate=0;
            var allReviewsQuery = await _unit.review.GetAllAsync();
            var reviewEntities = await allReviewsQuery.Where(r => r.ProductId == productId).ToListAsync();
            var resultProducts = _mapper.Map<IEnumerable<GetReviewDto>>(reviewEntities);
            foreach (var item in reviewEntities)
            {
                rate += (item.priceRating + item.qualityRating + item.valueRating) / 3;
            }
            return new ReviewListDto<GetReviewDto>() { entities = resultProducts ,Rating=rate/reviewEntities.Count() };
        }
        public async Task<bool> ReviewExist(Guid reviewId)
        {
            return await _unit.review.EntityExist(reviewId);
        }
    }
}
