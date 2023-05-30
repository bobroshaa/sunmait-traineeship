using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<CustomerOrder>> GetAll();
    Task<IEnumerable<OrderProduct>> GetAllByOrderId(int orderId);
    Task<CustomerOrder?> GetById(int id);
    Task Add(CustomerOrder order);
    void Update(CustomerOrder updatingOrder, Status orderStatus);
    void Delete(CustomerOrder order);
    Task AddOrderItem(OrderProduct orderItem, Product product);
    void DeleteOrderItemFromOrder(OrderProduct orderItem, Product? product);
    Task<OrderProduct?> GetOrderItemById(int id);
    Task Save();
}