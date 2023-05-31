using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _reviewRepository = reviewRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<ReviewViewModel?> GetById(int id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review is null)
        {
            throw new Exception(ExceptionMessages.ReviewNotFound);
        }

        return _mapper.Map<ReviewViewModel>(review);
    }

    public async Task<List<ReviewViewModel>> GetReviewsByProductId(int productId)
    {
        var product = await _productRepository.GetById(productId);
        if (product is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        return _mapper.Map<List<ReviewViewModel>>(await _reviewRepository.GetReviewByProductId(productId));
    }

    public async Task<int> Add(ReviewInputModel reviewInputModel)
    {
        var product = await _productRepository.GetById(reviewInputModel.ProductID);
        if (product is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        var user = await _productRepository.GetById(reviewInputModel.UserID);
        if (user is null)
        {
            throw new Exception(ExceptionMessages.UserNotFound);
        }

        var review = _mapper.Map<Review>(reviewInputModel);
        review.AddDate = DateTime.UtcNow;
        await _reviewRepository.Add(review);
        return review.ID;
    }

    public async Task Update(int id, ReviewInputModel reviewInputModel)
    {
        var updatingReview = await _reviewRepository.GetById(id);
        if (updatingReview is null)
        {
            throw new Exception(ExceptionMessages.ReviewNotFound);
        }

        await _reviewRepository.Update(updatingReview, _mapper.Map<Review>(reviewInputModel));
    }

    public async Task Delete(int id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review is null)
        {
            throw new Exception(ExceptionMessages.ReviewNotFound);
        }

        await _reviewRepository.Delete(review);
    }
}