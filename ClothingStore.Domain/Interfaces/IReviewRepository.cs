using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetById(int id);
    Task<List<Review>> GetReviewByProductId(int productId);
    Task Add(Review review);
    Task Update(Review updatingReview, Review review);
    Task Delete(Review review);
}