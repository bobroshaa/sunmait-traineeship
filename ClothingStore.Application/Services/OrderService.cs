using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<OrderViewModel>> GetAll()
    {
        return _mapper.Map<List<OrderViewModel>>(await _orderRepository.GetAll());
    }

    public async Task<List<OrderItemViewModel>> GetOrderItemsByOrderId(int orderId)
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
        foreach (var item in orderInputModel.Products)
        {
            await AddOrderItemInOrder(order.ID, item);
        }

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

    public async Task AddOrderItemInOrder(int orderId, OrderItemInputModel orderItemInputModel)
    {
        var product = await _productRepository.GetById(orderItemInputModel.ProductID);
        if (product is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        if (orderItemInputModel.Quantity > product.Quantity)
        {
            throw new Exception(ExceptionMessages.ProductQuantityIsNotAvailable);
        }

        var mappedItem = _mapper.Map<OrderProduct>(orderItemInputModel);
        mappedItem.OrderID = orderId;
        await _orderRepository.AddOrderItem(mappedItem, product);
    }

    public async Task DeleteOrderItemFromOrder(int orderItemId)
    {
        var orderItem = await _orderRepository.GetOrderItemById(orderItemId);
        if (orderItem is null)
        {
            throw new Exception(ExceptionMessages.OrderItemNotFound);
        }

        var product = await _productRepository.GetById(orderItem.ProductID);
        await _orderRepository.DeleteOrderItemFromOrder(orderItem, product);
    }
}