using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ISectionRepository
{
    Task<List<Section>> GetAll();
    Task<Section?> GetById(int id);
    Task Add(Section section);
    Task Update(Section updatingSection, Section section);
    Task Delete(Section section);
}