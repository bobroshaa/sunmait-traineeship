using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface ICategoryService
{
    public Task<CategoryViewModel?> GetById(int id);
    public Task<int> Add(CategoryInputModel categoryInputModel);
    public Task Update(int id, CategoryInputModel categoryInputModel);
    public Task Delete(int id);
}