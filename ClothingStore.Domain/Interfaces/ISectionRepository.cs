using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ISectionRepository
{
    Task<List<Section>> GetAll();
    Task<Section?> GetById(int id);
    void Add(Section section);
    Task Save();
    void Delete(Section section);
    Task<bool> DoesSectionExist(int id);
}