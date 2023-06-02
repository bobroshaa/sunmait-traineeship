using AutoMapper;
using ClothingStore.Application.Exceptions;
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
        var review = await GetReviewById(id);
        return _mapper.Map<ReviewViewModel>(review);
    }

    public async Task<List<ReviewViewModel>> GetReviewsByProductId(int productId)
    {
        await ValidateProduct(productId);
        return _mapper.Map<List<ReviewViewModel>>(await _reviewRepository.GetReviewByProductId(productId));
    }

    public async Task<int> Add(ReviewInputModel reviewInputModel)
    {
        await ValidateProduct(reviewInputModel.ProductID);
        await ValidateUser(reviewInputModel.UserID);
        var review = _mapper.Map<Review>(reviewInputModel);
        _reviewRepository.Add(review);
        await _reviewRepository.Save();
        return review.ID;
    }

    public async Task Update(int id, ReviewInputModel reviewInputModel)
    {
        var review = await GetReviewById(id);
        review.Comment = reviewInputModel.Comment;
        review.Rating = reviewInputModel.Rating;
        review.ReviewTitle = reviewInputModel.ReviewTitle;
        
        await _reviewRepository.Save();
    }

    public async Task Delete(int id)
    {
        var review = await GetReviewById(id);
        _reviewRepository.Delete(review);
        await _reviewRepository.Save();
    }
    
    private async Task<Review> GetReviewById(int id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ReviewNotFound, id));
        }

        return review;
    }
    
    private async Task ValidateProduct(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }
    }
    
    private async Task ValidateUser(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }
    }
}