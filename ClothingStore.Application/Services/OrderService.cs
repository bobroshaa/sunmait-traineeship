using AutoMapper;
using ClothingStore.Application.Exceptions;
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
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, orderId));
        }

        return _mapper.Map<List<OrderItemViewModel>>(await _orderRepository.GetAllByOrderId(orderId));
    }

    public async Task<OrderViewModel?> GetById(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, id));
        }

        return _mapper.Map<OrderViewModel>(order);
    }

    public async Task<int> Add(OrderInputModel orderInputModel)
    {
        var order = _mapper.Map<CustomerOrder>(orderInputModel);
        order.OrderDate = DateTime.UtcNow;
        order.CurrentStatus = Status.InReview;
        var productsIds = orderInputModel.Products.Select(p => p.ProductID).ToList();
        var user = await _userRepository.GetById(order.UserID);
        if (user is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, order.UserID));
        }

        var products = await _productRepository.GetProductsByIds(productsIds);
        if (products.Count < productsIds.Count)
        {
            // TODO: Find id of non-existing product
            throw new EntityNotFoundException(ExceptionMessages.ProductNotFound);
        }

        var product =
            orderInputModel.Products.FirstOrDefault(item =>
                item.Quantity > products.First(p => p.ID == item.ProductID).Quantity);
        if (product is not null)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.ProductQuantityIsNotAvailable,
                product.ProductID, products.First(p => p.ID == product.ProductID).Quantity));
        }

        order.OrderProducts = new List<OrderProduct>();
        foreach (var item in orderInputModel.Products)
        {
            var mappedItem = _mapper.Map<OrderProduct>(item);
            _orderRepository.AddOrderItem(mappedItem, products.First(p => p.ID == item.ProductID));
        }

        _orderRepository.Add(order);
        await _orderRepository.Save();
        return order.ID;
    }

    public async Task Update(int id, Status orderStatus)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, id));
        }

        _orderRepository.Update(order, orderStatus);
        await _orderRepository.Save();
    }

    public async Task Delete(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, id));
        }

        _orderRepository.Delete(order);
        await _orderRepository.Save();
    }

    public async Task<int> AddOrderItemToOrder(int orderId, OrderItemInputModel orderItemInputModel)
    {
        var order = await _orderRepository.GetById(orderId);
        if (order is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, orderId));
        }

        var product = await _productRepository.GetById(orderItemInputModel.ProductID);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound,
                orderItemInputModel.ProductID));
        }

        if (orderItemInputModel.Quantity > product.Quantity)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.ProductQuantityIsNotAvailable,
                product.ID, product.Quantity));
        }

        var mappedItem = _mapper.Map<OrderProduct>(orderItemInputModel);
        mappedItem.OrderID = orderId;
        _orderRepository.AddOrderItem(mappedItem, product);
        await _orderRepository.Save();
        return mappedItem.ID;
    }

    public async Task DeleteOrderItemFromOrder(int orderItemId)
    {
        var orderItem = await _orderRepository.GetOrderItemById(orderItemId);
        if (orderItem is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderItemNotFound, orderItemId));
        }

        var product = await _productRepository.GetById(orderItem.ProductID);
        _orderRepository.DeleteOrderItemFromOrder(orderItem, product);
        await _orderRepository.Save();
    }
}