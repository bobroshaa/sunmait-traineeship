using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Add(Product product);
    Task Update(Product updatingProduct, Product product);
    Task Delete(Product product);
    Task<List<Product>> GetProductsBySectionAndCategory(int sectionId, int categoryId);
    Task<List<Product>> GetProductsByBrand(int brandId);
    Task<Dictionary<int, Product>> GetProductsByIds(List<int> productIds);
    Task AssignToBrand(Product product, int brandId);
    Task UnassignFromBrand(Product product);
}