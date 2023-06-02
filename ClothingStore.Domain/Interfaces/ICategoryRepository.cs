using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetById(int id);
    void Add(Category category);
    Task SaveChanges();
    void Delete(Category category);
    void LinkCategoryToSection(SectionCategory sectionCategory);
    Task<bool> DoesSectionCategoryExist(int sectionId, int categoryId);
    Task<SectionCategory?> GetSectionCategoryById(int id);
    Task<bool> DoesCategoryExist(int id);
    Task<bool> DoesSectionCategoryExist(int id);
}