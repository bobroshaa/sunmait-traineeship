using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ICartRepository
{
    Task<List<CartItem>> GetUserCart(int userId);
    Task<CartItem?> GetById(int id);
    void Add(CartItem cartItem);
    void Delete(CartItem cartItem);
    Task SaveChanges();
}