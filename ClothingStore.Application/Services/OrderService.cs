using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
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
        var productsIds = orderInputModel.Products.Select(p => p.ProductID).ToList();
        var user = await _userRepository.GetById(order.UserID);
        if (user is null)
        {
            throw new Exception(ExceptionMessages.UserNotFound);
        }

        var products = await _productRepository.GetProductsByIds(productsIds);
        if (products.Count < productsIds.Count)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        if (orderInputModel.Products.Any(item => item.Quantity > products.First(p => p.ID == item.ProductID).Quantity))
        {
            throw new Exception(ExceptionMessages.ProductQuantityIsNotAvailable);
        }

        order.OrderProducts = new List<OrderProduct>();
        foreach (var item in orderInputModel.Products)
        {
            var mappedItem = _mapper.Map<OrderProduct>(item);
            await _orderRepository.AddOrderItem(mappedItem, products.First(p => p.ID == item.ProductID));
        }

        await _orderRepository.Add(order);
        await _orderRepository.Save();
        return order.ID;
    }

    public async Task Update(int id, Status orderStatus)
    {
        var updatingOrder = await _orderRepository.GetById(id);
        if (updatingOrder is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }

        _orderRepository.Update(updatingOrder, orderStatus);
        await _orderRepository.Save();
    }

    public async Task Delete(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }

        _orderRepository.Delete(order);
        await _orderRepository.Save();
    }

    public async Task<int> AddOrderItemInOrder(int orderId, OrderItemInputModel orderItemInputModel)
    {
        var order = await _orderRepository.GetById(orderId);
        if (order is null)
        {
            throw new Exception(ExceptionMessages.OrderNotFound);
        }

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
        await _orderRepository.Save();
        return mappedItem.ID;
    }

    public async Task DeleteOrderItemFromOrder(int orderItemId)
    {
        var orderItem = await _orderRepository.GetOrderItemById(orderItemId);
        if (orderItem is null)
        {
            throw new Exception(ExceptionMessages.OrderItemNotFound);
        }

        var product = await _productRepository.GetById(orderItem.ProductID);
        _orderRepository.DeleteOrderItemFromOrder(orderItem, product);
        await _orderRepository.Save();
    }
}