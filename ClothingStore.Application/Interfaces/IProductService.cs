using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IProductService
{
    public Task<List<ProductViewModel>> GetAll();
    public Task<ProductViewModel?> GetById(int id);
    public Task<int> Add(ProductInputModel productInputModel);
    public Task Update(int id, ProductInputModel productInputModel);
    public Task Delete(int id);
    Task<List<ProductViewModel>> GetProductsBySectionAndCategory(int sectionId, int categoryId);
    Task<List<ProductViewModel>> GetProductsByBrand(int brandId);
    Task AssignToBrand(int productId, int brandId);
    Task UnassignFromBrand(int productId);
}