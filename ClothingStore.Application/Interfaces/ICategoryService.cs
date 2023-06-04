using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryViewModel?> GetById(int id);
    Task<PostResponseViewModel> Add(CategoryInputModel categoryInputModel);
    Task Update(int id, CategoryInputModel categoryInputModel);
    Task LinkCategoryToSection(int sectionId, int categoryId);
    Task Delete(int id);
}