using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly Context _dbContext;

    public BrandRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Brand>> GetAll()
    {
        return await _dbContext.Brands.Where(b => b.IsActive).ToListAsync();
    }

    public async Task<Brand?> GetById(int id)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(b => b.ID == id && b.IsActive);
    }

    public void Add(Brand brand)
    {
        _dbContext.Brands.Add(brand);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Delete(Brand brand)
    {
        brand.IsActive = false;
    }

    public async Task<bool> DoesBrandExist(string name)
    {
        return await _dbContext.Brands.AnyAsync(b => b.Name == name && b.IsActive);
    }
    
    public async Task<bool> DoesBrandExist(int id)
    {
        return await _dbContext.Brands.AnyAsync(b => b.ID == id && b.IsActive);
    }
}