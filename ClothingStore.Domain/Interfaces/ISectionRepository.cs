using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ISectionRepository
{
    Task<Section?> GetById(int id);
    Task Add(Section section);
    Task Update(Section updatingSection, string newName);
}