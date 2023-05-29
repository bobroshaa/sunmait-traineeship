using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly Context _dbContext;

    public SectionRepository(Context dbContext)
    {
        _dbContext = dbContext;
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
}