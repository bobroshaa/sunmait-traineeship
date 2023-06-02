using AutoMapper.Configuration.Conventions;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly Context _dbContext;

    public CategoryRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category?> GetById(int id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(b => b.ID == id && b.IsActive);
    }

    public void Add(Category category)
    {
        _dbContext.Categories.Add(category);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Delete(Category category)
    {
        category.IsActive = false;
    }

    public void LinkCategoryToSection(SectionCategory sectionCategory)
    {
        _dbContext.SectionCategories.Add(sectionCategory);
    }
    
    public async Task<bool> DoesSectionCategoryExist(int sectionId, int categoryId)
    {
        return await _dbContext.SectionCategories.AnyAsync(sc => sc.CategoryID == categoryId && sc.SectionID == sectionId);
    }

    public async Task<SectionCategory?> GetSectionCategoryById(int id)
    {
        return await _dbContext.SectionCategories.FirstOrDefaultAsync(sc => sc.ID == id);
    }
    
    public async Task<bool> DoesCategoryExist(int id)
    {
        return await _dbContext.Categories.AnyAsync(c => c.ID == id && c.IsActive);
    }

    public async Task<bool> DoesSectionCategoryExist(int id)
    {
        return await _dbContext.SectionCategories.AnyAsync(sc => sc.ID == id);
    }
}