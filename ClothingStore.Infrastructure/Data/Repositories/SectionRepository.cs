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
        return await _dbContext.Sections.ToListAsync();
    }
    
    public async Task<Section?> GetById(int id)
    {
        return await _dbContext.Sections.FirstOrDefaultAsync(s => s.ID == id);
    }
    
    public async Task Add(Section section)
    {
        await _dbContext.Sections.AddAsync(section);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Section updatingSection, string newName)
    {
        updatingSection.Name = newName;
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(Section section)
    {
        /*section.IsActive = false;
        await _dbContext.SaveChangesAsync();*/
    }
}