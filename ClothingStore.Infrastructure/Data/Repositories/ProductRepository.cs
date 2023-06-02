using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Context _dbContext;

    public ProductRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetAll()
    {
        return await _dbContext.Products.Where(p => p.IsActive).ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ID == id && p.IsActive);
        return product;
    }

    public void Add(Product product)
    {
        product.AddDate = DateTime.UtcNow;
        _dbContext.Products.Add(product);
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Delete(Product product)
    {
        product.IsActive = false;
    }

    public async Task<List<Product>> GetProductsBySectionAndCategory(int sectionId, int categoryId)
    {
        return await _dbContext
            .Products
            .Where(p => p.IsActive && p.SectionCategory.CategoryID == categoryId &&
                        p.SectionCategory.SectionID == sectionId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByBrand(int brandId)
    {
        return await _dbContext.Products.Where(p => p.IsActive && p.BrandID == brandId).ToListAsync();
    }

    public async Task<Dictionary<int, Product>> GetProductsByIds(List<int> productIds)
    {
        var products = await _dbContext
            .Products
            .Where(p => productIds.Contains(p.ID))
            .ToListAsync();
        return products.ToDictionary(keySelector: p => p.ID, elementSelector: p => p);
    }

    public void AssignToBrand(Product product, int brandId)
    {
        product.BrandID = brandId;
    }

    public void UnassignFromBrand(Product product)
    {
        product.BrandID = null;
    }

    public async Task<bool> DoesProductExist(int id)
    {
        return await _dbContext.Products.AnyAsync(p => p.ID == id && p.IsActive);
    }
}