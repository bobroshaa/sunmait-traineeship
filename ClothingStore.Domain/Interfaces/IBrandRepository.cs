using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IBrandRepository
{
    Task<List<Brand>> GetAll();
    Task<Brand?> GetById(int id);
    void Add(Brand brand);
    Task Save();
    void Delete(Brand brand);
    Task<bool> DoesBrandExist(string name);
}