using ClothingStore.Application.Models;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IOrderItemService
{
    Task<OrderItemViewModel?> GetById(int id);
    Task<int> Add(OrderItemInputModel orderItemInputModel);
    Task Update(int id, OrderItemInputModel orderItemInputModel);
    Task Delete(int id);
}