using ClothingStore.Application.Models;

namespace ClothingStore.Application.Interfaces;

public interface IBrandService
{
    public Task<List<BrandViewModel>> GetAll();
    public Task<BrandViewModel?> GetById(int id);
    public Task<int> Add(BrandInputModel brandInputModel);
    public Task Update(int id, BrandInputModel brandInputModel);
    public Task Delete(int id);
}