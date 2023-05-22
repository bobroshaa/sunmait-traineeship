using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly Context _dbContext;

    public BrandRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Brand>> GetAll()
    {
        return await _dbContext.Brands.Where(b => b.IsActive).ToListAsync();
    }

    public async Task<Brand?> GetById(int id)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(b => b.ID == id & b.IsActive);
    }

    public async Task Add(Brand brand)
    {
        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Brand updatingBrand, Brand brand)
    {
        updatingBrand.Name = brand.Name;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Brand brand)
    {
        brand.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}