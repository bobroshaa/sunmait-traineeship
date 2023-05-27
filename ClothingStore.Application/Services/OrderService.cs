using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    public OrderService(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }
    
    public async Task<IEnumerable<OrderViewModel>> GetAll()
    {
        return _mapper.Map<List<OrderViewModel>>(await _orderRepository.GetAll());
    }

    public async Task<List<OrderItemViewModel>> GetAllByOrderId(int orderId)
    {
        var order = await _orderRepository.GetById(orderId);
        if (order is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }
        return _mapper.Map<List<OrderItemViewModel>>(await _orderRepository.GetAllByOrderId(orderId));
    }
    
    public async Task<OrderViewModel?> GetById(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }

        return _mapper.Map<OrderViewModel>(order);
    }

    public async Task<int> Add(OrderInputModel orderInputModel)
    {
        var order = _mapper.Map<CustomerOrder>(orderInputModel);
        order.OrderDate = DateTime.UtcNow;
        await _orderRepository.Add(order);
        return order.ID;
    }

    public async Task Update(int id, OrderInputModel orderInputModel)
    {
        var updatingOrder = await _orderRepository.GetById(id);
        if (updatingOrder is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }

        await _orderRepository.Update(updatingOrder, _mapper.Map<CustomerOrder>(orderInputModel));
    }

    public async Task Delete(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }

        await _orderRepository.Delete(order);
    }
}