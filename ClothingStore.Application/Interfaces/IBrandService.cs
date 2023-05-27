using ClothingStore.Application.Models;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IBrandService
{
    public Task<List<BrandViewModel>> GetAll();
    public Task<BrandViewModel?> GetById(int id);
    public Task<int> Add(BrandInputModel brandInputModel);
    public Task Update(int id, BrandInputModel brandInputModel);
    public Task Delete(int id);
}