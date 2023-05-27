using ClothingStore.Domain.Entities;
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
        return await _dbContext.CustomerOrders.ToListAsync();
    }

    public async Task<CustomerOrder?> GetById(int id)
    {
        return await _dbContext.CustomerOrders.FirstOrDefaultAsync(co => co.ID == id);
    }

    public async Task Add(CustomerOrder order)
    {
        await _dbContext.CustomerOrders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Update(CustomerOrder updatingOrder, CustomerOrder order)
    {
        updatingOrder.CurrentStatus = order.CurrentStatus;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(CustomerOrder order)
    {
        // product.IsActive = false;
        // await _dbContext.SaveChangesAsync();
    }
}