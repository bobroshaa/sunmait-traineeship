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

    public async Task<List<OrderViewModel>> GetAll()
    {
        return _mapper.Map<List<OrderViewModel>>(await _orderRepository.GetAll());
    }

    public async Task<List<OrderItemViewModel>> GetOrderItemsByOrderId(int orderId)
    {
        await GetOrderById(orderId);
        return _mapper.Map<List<OrderItemViewModel>>(await _orderRepository.GetAllByOrderId(orderId));
    }

    public async Task<OrderViewModel?> GetById(int id)
    {
        var order = await GetOrderById(id);
        return _mapper.Map<OrderViewModel>(order);
    }

    public async Task<int> Add(OrderInputModel orderInputModel)
    {
        var order = _mapper.Map<CustomerOrder>(orderInputModel);
        order.CurrentStatus = Status.InReview;
        var productsIds = orderInputModel.Products.Select(p => p.ProductID).ToList();
        await ValidateUser(order.UserID);
        var products = await _productRepository.GetProductsByIds(productsIds);
        if (products.Count < productsIds.Count)
        {
            // TODO: Find id of non-existing product
            // TODO: Add validation in private method
            throw new EntityNotFoundException(ExceptionMessages.ProductNotFound);
        }

        order.OrderProducts = new List<OrderProduct>();
        foreach (var item in orderInputModel.Products)
        {
            ValidateProductQuantity(item.Quantity, products[item.ProductID].Quantity);
            order.OrderProducts.Add(_mapper.Map<OrderProduct>(item));
            products[item.ProductID].Quantity -= item.Quantity;
        }

        _orderRepository.Add(order);
        await _orderRepository.Save();
        return order.ID;
    }

    public async Task Update(int id, Status orderStatus)
    {
        var order = await GetOrderById(id);
        ValidateOrderStatus(orderStatus, order.CurrentStatus);
        _orderRepository.Update(order, orderStatus);
        await _orderRepository.Save();
    }

    public async Task Delete(int id)
    {
        var order = await GetOrderById(id);
        _orderRepository.Delete(order);
        await _orderRepository.Save();
    }

    private async Task<CustomerOrder> GetOrderById(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, id));
        }

        return order;
    }

    private async Task ValidateUser(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }
    }
    
    private void ValidateProductQuantity(int requiredQuantity, int availableQuantity)
    {
        if (requiredQuantity > availableQuantity)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.ProductQuantityIsNotAvailable,
                requiredQuantity, availableQuantity));
        }
    }
    
    private void ValidateOrderStatus(Status currentStatus, Status newStatus)
    {
        if (newStatus - currentStatus != 1)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.IncorrectStatus,
                Enum.GetName(currentStatus), Enum.GetName(newStatus)));
        }
    }
}