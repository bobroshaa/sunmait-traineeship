using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly Context _dbContext;

    public OrderItemRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<OrderProduct>> GetAllById(int id)
    {
        return await _dbContext.OrderProducts
            .Where(op => op.OrderID == id)
            .ToListAsync();
    }
    
    public async Task<OrderProduct?> GetById(int id)
    {
        return await _dbContext.OrderProducts.FirstOrDefaultAsync(op => op.ID == id);
    }
    
    public async Task Add(OrderProduct orderItem)
    {
        await _dbContext.OrderProducts.AddAsync(orderItem);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Update(OrderProduct updatingOrderItem, OrderProduct orderItem)
    {
        updatingOrderItem.Price = orderItem.Price;
        updatingOrderItem.Quantity = orderItem.Quantity;
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(OrderProduct orderItem)
    {
        // orderItem.IsActive = false;
        // await _dbContext.SaveChangesAsync();
    }
}