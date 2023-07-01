using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderItemViewModel>> GetOrderItemsByOrderId(int orderId);
    Task<List<OrderViewModel>> GetAll();
    Task<OrderViewModel?> GetById(int id);
    Task<List<CartItemViewModel>> Add(OrderInputModel order);
    Task Update(int id, Status orderStatus);
    Task<List<ProductViewModel>> Delete(int id);
    Task<List<OrderHistoryViewModel>> GetOrderHistoryByOrderId(int orderId);
}