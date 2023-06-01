using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<CustomerOrder>> GetAll();
    Task<IEnumerable<OrderProduct>> GetAllByOrderId(int orderId);
    Task<CustomerOrder?> GetById(int id);
    void Add(CustomerOrder order);
    void Update(CustomerOrder updatingOrder, Status orderStatus);
    void Delete(CustomerOrder order);
    Task Save();
}