using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IProductService
{
    public Task<List<ProductViewModel>> GetAll();
    Task<ProductViewModel?> GetById(int id);
    Task<PostResponseViewModel> Add(ProductInputModel productInputModel);
    Task Update(int id, ProductInputModel productInputModel);
    Task Delete(int id);
    Task<List<ProductViewModel>> GetProductsBySectionAndCategory(int sectionId, int categoryId);
    Task<List<ProductViewModel>> GetProductsByBrand(int brandId);
    Task AssignToBrand(int productId, int brandId);
    Task UnassignFromBrand(int productId);
}