using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly Context _dbContext;

    public SectionRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Section>> GetAll()
    {
        return await _dbContext.Sections.Where(s => s.IsActive).ToListAsync();
    }
    
    public async Task<Section?> GetById(int id)
    {
        return await _dbContext.Sections.FirstOrDefaultAsync(s => s.ID == id && s.IsActive);
    }
    
    public void Add(Section section)
    {
        _dbContext.Sections.Add(section);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
    
    public void Delete(Section section)
    {
        section.IsActive = false;
    }
}