using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class CartRepository : ICartRepository
{
    private readonly Context _dbContext;

    public CartRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CartItem>> GetUserCart(int userId)
    {
        return await _dbContext
            .CartItems
            .Where(ci => ci.UserID == userId && ci.IsActive)
            .Include(ci => ci.Product)
            .ToListAsync();
    }

    public async Task<CartItem?> GetById(int id)
    {
        return await _dbContext.CartItems.Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.ID == id && ci.IsActive);
    }

    public async Task<CartItem?> GetByUserAndProduct(int userId, int productId)
    {
        return await _dbContext
            .CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.UserID == userId && ci.ProductID == productId && ci.IsActive);
    }

    public void Add(CartItem cartItem)
    {
        _dbContext.CartItems.Add(cartItem);
    }

    public void Delete(CartItem cartItem)
    {
        cartItem.IsActive = false;
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CartItem>> DeleteExpired()
    {
        var deletedCartItems = new List<CartItem>();

        var cartItems = await _dbContext.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.IsActive && ci.ReservationEndDate < DateTime.UtcNow)
            .ToListAsync();

        foreach (var ci in cartItems)
        {
            ci.IsActive = false;
            ci.Product.ReservedQuantity -= ci.Quantity;
            deletedCartItems.Add(ci);
            Console.WriteLine(ci.ID);
        }

        return deletedCartItems;
    }

    public async Task<Dictionary<int, CartItem>> GetCartItemsByIds(List<int> cartItemIds)
    {
        var cartItems = await _dbContext
            .CartItems
            .Include(ci => ci.Product)
            .Where(ci => cartItemIds.Contains(ci.ID))
            .ToListAsync();
        return cartItems.ToDictionary(ci => ci.ID, ci => ci);
    }
}