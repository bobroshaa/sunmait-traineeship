using Domain.Entities;

namespace Application.Interfaces;

public interface IBrandService
{
    public Task<IEnumerable<Brand>> GetAll();
    public Task<Brand?> GetById(int id);
    public Task<bool> Add(Brand brand);
    public Task<bool> Update(int id, Brand brand);
    public Task<bool> Delete(int id);
}