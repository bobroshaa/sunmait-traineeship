using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Add(Product product);
    Task Update(Product updatingProduct, Product product);
    Task Delete(Product product);
    Task<IEnumerable<Product>> GetProductsBySectionAndCategory(int sectionId, int categoryId);
    Task<IEnumerable<Product>> GetProductsByBrand(int brandId);
    
}