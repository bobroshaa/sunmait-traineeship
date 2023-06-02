using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IReviewService
{
    Task<ReviewViewModel?> GetById(int id);
    Task<List<ReviewViewModel>> GetReviewsByProductId(int productId);
    Task<int> Add(ReviewInputModel reviewInputModel);
    Task Update(int id, ReviewInputModel reviewInputModel);
    Task Delete(int id);
}