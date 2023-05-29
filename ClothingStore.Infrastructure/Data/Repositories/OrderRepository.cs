﻿using ClothingStore.Domain.Entities;
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
        order.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OrderProduct?> GetOrderItemById(int id)
    {
        return await _dbContext.OrderProducts.FirstOrDefaultAsync(op => op.ID == id && op.IsActive);
    }

    public async Task AddOrderItem(OrderProduct orderItem, Product product)
    {
        await _dbContext.OrderProducts.AddAsync(orderItem);
        product.Quantity -= orderItem.Quantity;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderItemFromOrder(OrderProduct orderItem, Product? product)
    {
        if (product is not null)
        {
            product.Quantity += orderItem.Quantity;
        }
        orderItem.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}