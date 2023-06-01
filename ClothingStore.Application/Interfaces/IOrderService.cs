using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderItemViewModel>> GetOrderItemsByOrderId(int orderId);
    Task<IEnumerable<OrderViewModel>> GetAll();
    Task<OrderViewModel?> GetById(int id);
    Task<int> Add(OrderInputModel order);
    Task Update(int id, Status orderStatus);
    Task Delete(int id);
}