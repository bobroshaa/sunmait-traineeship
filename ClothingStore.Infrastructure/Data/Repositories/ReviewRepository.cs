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

    public void Add(Review review)
    {
        review.AddDate = DateTime.UtcNow;
        _dbContext.Reviews.Add(review);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Delete(Review review)
    {
        review.IsActive = false;
    }
}