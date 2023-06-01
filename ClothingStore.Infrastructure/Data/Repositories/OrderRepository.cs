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

    public async Task<IEnumerable<CustomerOrder>> GetAll()
    {
        return await _dbContext.CustomerOrders.Where(co => co.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<OrderProduct>> GetAllByOrderId(int orderId)
    {
        return await _dbContext.OrderProducts
            .Where(op => op.OrderID == orderId && op.IsActive)
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
}