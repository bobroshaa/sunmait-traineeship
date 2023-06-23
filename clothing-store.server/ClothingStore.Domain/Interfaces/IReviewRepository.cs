using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetById(int id);
    Task<List<Review>> GetReviewByProductId(int productId);
    void Add(Review review);
    Task SaveChanges();
    void Delete(Review review);
}