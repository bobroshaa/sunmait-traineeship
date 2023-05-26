using ClothingStore.Application.Models;

namespace ClothingStore.Application.Interfaces;

public interface IProductService
{
    public Task<List<ProductViewModel>> GetAll();
    public Task<ProductViewModel?> GetById(int id);
    public Task<int> Add(ProductInputModel productInputModel);
    public Task Update(int id, ProductInputModel productInputModel);
    public Task Delete(int id);
}