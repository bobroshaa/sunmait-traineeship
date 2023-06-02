using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IBrandService
{
    Task<List<BrandViewModel>> GetAll();
    Task<BrandViewModel?> GetById(int id);
    Task<int> Add(BrandInputModel brandInputModel);
    Task Update(int id, BrandInputModel brandInputModel);
    Task Delete(int id);
}