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

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _dbContext.Products
            .Where(p => p.IsActive)
            .Include(p => p.Brand)
            .Include(p => p.SectionCategory.Category)
            .Include(p => p.SectionCategory.Section)
            .ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        var product = await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.SectionCategory.Category)
            .Include(p => p.SectionCategory.Section)
            .FirstOrDefaultAsync(p => p.ID == id && p.IsActive);
        return product;
    }

    public async Task Add(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Product updatingProduct, Product product)
    {
        updatingProduct.Name = product.Name;
        updatingProduct.Price = product.Price;
        updatingProduct.Quantity = product.Quantity;
        updatingProduct.Description = product.Description;
        updatingProduct.ImageURL = product.ImageURL;
        updatingProduct.SectionCategoryID = product.SectionCategoryID;
        updatingProduct.BrandID = product.BrandID;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        product.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsBySectionAndCategory(int sectionId, int categoryId)
    {
        return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.SectionCategory.Category)
            .Include(p => p.SectionCategory.Section)
            .Where(p => p.IsActive && p.SectionCategory.CategoryID == categoryId &&
                        p.SectionCategory.SectionID == sectionId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetProductsByBrand(int brandId)
    {
        return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.SectionCategory.Category)
            .Include(p => p.SectionCategory.Section)
            .Where(p => p.IsActive && p.BrandID == brandId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByIds(IEnumerable<int> productIds)
    {
        return await _dbContext.Products
            .Where(p => productIds.Contains(p.ID))
            .ToListAsync();
    }
    
    public async Task AssignToBrand(Product product, int brandId)
    {
        product.BrandID = brandId;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UnassignFromBrand(Product product)
    {
        product.BrandID = null;
        await _dbContext.SaveChangesAsync();
    }
}