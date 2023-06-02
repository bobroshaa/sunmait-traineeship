using System.Reflection.Metadata.Ecma335;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Context _dbContext;

    public OrderRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CustomerOrder>> GetAll()
    {
        return await _dbContext.CustomerOrders.Where(co => co.IsActive).ToListAsync();
    }

    public async Task<List<OrderProduct>> GetAllByOrderId(int orderId)
    {
        return await _dbContext.OrderProducts
            .Where(op => op.OrderID == orderId)
            .ToListAsync();
    }
    public async Task<CustomerOrder?> GetById(int id)
    {
        return await _dbContext.CustomerOrders.FirstOrDefaultAsync(co => co.ID == id && co.IsActive);
    }

    public void Add(CustomerOrder order)
    {
        order.OrderDate = DateTime.UtcNow;
        _dbContext.CustomerOrders.Add(order);
    }
    
    public void Update(CustomerOrder updatingOrder, Status orderStatus)
    {
        updatingOrder.CurrentStatus = orderStatus;
    }

    public void Delete(CustomerOrder order)
    {
        order.IsActive = false;
    }
    
    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<OrderHistory>> GetOrderHistoryByOrderId(int orderId)
    {
        return await _dbContext.OrderHistories.Where(oh => oh.OrderID == orderId).ToListAsync();
    }

    public async Task<bool> DoesOrderExist(int id)
    {
        return await _dbContext.CustomerOrders.AnyAsync(o => o.ID == id && o.IsActive);
    }
}