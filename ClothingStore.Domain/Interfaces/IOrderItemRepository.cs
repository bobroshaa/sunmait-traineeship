﻿using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IOrderItemRepository
{
    Task<OrderProduct?> GetById(int id);
    Task Add(OrderProduct orderItem);
    Task Update(OrderProduct updatingOrderItem, OrderProduct orderItem);
    Task Delete(OrderProduct orderItem);
}