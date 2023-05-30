using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly Context _dbContext;

    public ReviewRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Review>> GetReviewByProductId(int productId)
    {
        return await _dbContext.Reviews.Where(r => r.ProductID == productId && r.IsActive).ToListAsync();
    }

    public async Task<Review?> GetById(int id)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(r => r.ID == id && r.IsActive);
    }

    public async Task Add(Review review)
    {
        await _dbContext.Reviews.AddAsync(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Review updatingReview, Review review)
    {
        updatingReview.Comment = review.Comment;
        updatingReview.Rating = review.Rating;
        updatingReview.ReviewTitle = review.ReviewTitle;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Review review)
    {
        review.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}