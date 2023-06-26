﻿using ClothingStore.Domain.Entities;
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
        return await _dbContext.CartItems.Include(ci => ci.Product).FirstOrDefaultAsync(ci => ci.ID == id && ci.IsActive);
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

    public async Task<Dictionary<int, int>> DeleteExpired()
    {
        var productIdsOfDeletedItems = new Dictionary<int, int>();
        
        await _dbContext
            .CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.IsActive && ci.ReservationEndDate < DateTime.UtcNow)
            .ForEachAsync(ci =>
            {
                ci.IsActive = false;
                
                ci.Product.ReservedQuantity -= ci.Quantity;
                ci.Product.InStockQuantity += ci.Quantity;
                
                productIdsOfDeletedItems.Add(ci.ProductID, ci.Product.ReservedQuantity);
                Console.WriteLine(ci.ID);
            });
        
        return productIdsOfDeletedItems;
    }
    
    public async Task<Dictionary<int, CartItem>> GetCartItemsByIds(List<int> cartItemIds)
    {
        var cartItems = await _dbContext
            .CartItems
            .Include(ci => ci.Product)
            .Where(ci => cartItemIds.Contains(ci.ID))
            .ToListAsync();
        return cartItems.ToDictionary( ci => ci.ID, ci => ci);
    }
}