using ClothingStore.Application.Models;

namespace ClothingStore.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderViewModel>> GetAll();
    Task<OrderViewModel?> GetById(int id);
    Task<int> Add(OrderInputModel order);
    Task Update(int id, OrderInputModel order);
    Task Delete(int id);
}