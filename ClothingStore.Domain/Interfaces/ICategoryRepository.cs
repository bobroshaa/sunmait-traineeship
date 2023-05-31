using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetById(int id);
    Task Add(Category category);
    Task Update(Category updatingCategory, Category category);
    Task Delete(Category category);
    public Task LinkCategoryToSection(SectionCategory sectionCategory);
    Task<bool> SectionCategoryIsUnique(int sectionId, int categoryId);
    Task<SectionCategory?> GetSectionCategoryById(int id);
}