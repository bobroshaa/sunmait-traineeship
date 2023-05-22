using Domain.Entities;

namespace Domain.Interfaces;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAll();
    Task<Brand?> GetById(int id);
    Task Add(Brand brand);
    Task Update(Brand updatingBrand, Brand brand);
    Task Delete(Brand brand);
}