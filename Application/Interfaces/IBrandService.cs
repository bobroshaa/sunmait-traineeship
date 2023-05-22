using Domain.Entities;

namespace Application.Interfaces;

public interface IBrandService
{
    public Task<IEnumerable<Brand>> GetAll();
    public Task<Brand?> GetById(int id);
    public Task Add(Brand brand);
    public Task Update(int id, Brand brand);
    public Task Delete(int id);
}