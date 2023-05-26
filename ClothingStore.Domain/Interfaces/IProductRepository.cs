using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Models;

namespace ClothingStore.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Add(Product product);
    Task Update(Product updatingProduct, Product product);
    Task Delete(Product product);
}