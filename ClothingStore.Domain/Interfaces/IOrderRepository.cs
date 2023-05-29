using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<CustomerOrder>> GetAll();
    Task<IEnumerable<OrderProduct>> GetAllByOrderId(int orderId);
    Task<CustomerOrder?> GetById(int id);
    Task Add(CustomerOrder order);
    Task Update(CustomerOrder updatingOrder, CustomerOrder order);
    Task Delete(CustomerOrder order);
    Task AddOrderItem(OrderProduct orderItem, Product product);
    Task DeleteOrderItemFromOrder(OrderProduct orderItem, Product? product);
    Task<OrderProduct?> GetOrderItemById(int id);
}