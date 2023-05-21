using Domain.Entities;

namespace Domain.Interfaces;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAll();
    Task<Brand?> GetById(int id);
    Task<bool> Add(Brand brand);
    Task<bool> Update(Brand updatingBrand, Brand brand);
    Task<bool> Delete(Brand brand);
}