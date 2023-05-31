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

    public async Task Add(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Category updatingCategory, Category category)
    {
        updatingCategory.Name = category.Name;
        updatingCategory.ParentCategoryID = category.ParentCategoryID;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Category category)
    {
        category.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task LinkCategoryToSection(SectionCategory sectionCategory)
    {
        await _dbContext.SectionCategories.AddAsync(sectionCategory);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> SectionCategoryIsUnique(int sectionId, int categoryId)
    {
        return await _dbContext.SectionCategories.FirstOrDefaultAsync(sc => sc.CategoryID == categoryId && sc.SectionID == sectionId) is null;
    }

    public async Task<SectionCategory?> GetSectionCategoryById(int id)
    {
        return await _dbContext.SectionCategories.FirstOrDefaultAsync(sc => sc.ID == id);
    }
}