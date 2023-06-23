using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product?> GetById(int id);
    void Add(Product product);
    Task SaveChanges();
    void Delete(Product product);
    Task<List<Product>> GetProductsBySectionAndCategory(int sectionId, int categoryId);
    Task<List<Product>> GetProductsByBrand(int brandId);
    Task<Dictionary<int, Product>> GetProductsByIds(List<int> productIds);
    void AssignToBrand(Product product, int brandId);
    void UnassignFromBrand(Product product);
    Task<bool> DoesProductExist(int id);
}