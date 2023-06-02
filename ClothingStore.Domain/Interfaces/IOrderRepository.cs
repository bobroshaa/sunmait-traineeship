using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<CustomerOrder>> GetAll();
    Task<List<OrderProduct>> GetAllByOrderId(int orderId);
    Task<CustomerOrder?> GetById(int id);
    void Add(CustomerOrder order);
    void Update(CustomerOrder updatingOrder, Status orderStatus);
    void Delete(CustomerOrder order);
    Task SaveChanges();
    Task<List<OrderHistory>> GetOrderHistoryByOrderId(int orderId);
    Task<bool> DoesOrderExist(int id);
}