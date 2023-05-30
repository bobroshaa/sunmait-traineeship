using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IReviewService
{
    public Task<ReviewViewModel?> GetById(int id);
    public Task<List<ReviewViewModel>> GetReviewsByProductId(int productId);
    public Task<int> Add(ReviewInputModel reviewInputModel);
    public Task Update(int id, ReviewInputModel reviewInputModel);
    public Task Delete(int id);
}