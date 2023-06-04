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

    public OrderService(
        IMapper mapper, 
        IOrderRepository orderRepository, 
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<List<OrderViewModel>> GetAll()
    {
        var orders = await _orderRepository.GetAll();
        var orderVms = _mapper.Map<List<OrderViewModel>>(orders);

        return orderVms;
    }

    public async Task<List<OrderItemViewModel>> GetOrderItemsByOrderId(int orderId)
    {
        await ValidateOrder(orderId);

        var orderItems = await _orderRepository.GetAllByOrderId(orderId);
        var orderItemVms = _mapper.Map<List<OrderItemViewModel>>(orderItems);
        
        return orderItemVms;
    }

    public async Task<OrderViewModel?> GetById(int id)
    {
        var order = await GetOrderById(id);
        var orderVm = _mapper.Map<OrderViewModel>(order);
        
        return orderVm;
    }

    public async Task<int> Add(OrderInputModel orderInputModel)
    {
        var order = _mapper.Map<CustomerOrder>(orderInputModel);
        
        var productsIds = orderInputModel.Products.Select(p => p.ProductID).ToList();
        
        var productsDictionary = await _productRepository.GetProductsByIds(productsIds);
        
        ValidateProducts(productsIds, productsDictionary.Keys.ToList());
        await ValidateUser(order.UserID);
        
        order.OrderProducts = new List<OrderProduct>();
        
        foreach (var itemInputModel in orderInputModel.Products)
        {
            if (productsDictionary.TryGetValue(itemInputModel.ProductID, out var productToUpdate))
            {
                ValidateProductQuantity(itemInputModel.ProductID, itemInputModel.Quantity, productToUpdate.Quantity);
                
                var item = _mapper.Map<OrderProduct>(itemInputModel);
                
                item.Price = productToUpdate.Price * item.Quantity;
                item.OrderID = order.ID;
                
                order.OrderProducts.Add(item);
                
                productToUpdate.Quantity -= itemInputModel.Quantity;
            }
            else
            {
                throw new EntityNotFoundException(
                    string.Format(ExceptionMessages.ProductNotFound, itemInputModel.ProductID));
            }
        }

        order.CurrentStatus = Status.InReview;

        _orderRepository.Add(order);

        await _orderRepository.SaveChanges();
        
        return order.ID;
    }

    public async Task Update(int id, Status orderStatus)
    {
        var order = await GetOrderById(id);
        
        ValidateOrderStatus(orderStatus);
        ValidateOrderStatusChanging(order.CurrentStatus, orderStatus);
        
        _orderRepository.Update(order, orderStatus);
        
        await _orderRepository.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var order = await GetOrderById(id);
        
        _orderRepository.Delete(order);
        
        await _orderRepository.SaveChanges();
    }

    public async Task<List<OrderHistoryViewModel>> GetOrderHistoryByOrderId(int orderId)
    {
        await ValidateOrder(orderId);
        
        var orderHistory = await _orderRepository.GetOrderHistoryByOrderId(orderId);
        var orderHistoryVms = _mapper.Map<List<OrderHistoryViewModel>>(orderHistory);

        return orderHistoryVms;
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
        if (!await _userRepository.DoesUserExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }
    }

    private async Task ValidateOrder(int id)
    {
        if (!await _orderRepository.DoesOrderExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.OrderNotFound, id));
        }
    }

    private void ValidateProductQuantity(int productId, int requiredQuantity, int availableQuantity)
    {
        if (requiredQuantity > availableQuantity)
        {
            throw new IncorrectParamsException(
                string.Format(ExceptionMessages.ProductQuantityIsNotAvailable, requiredQuantity, productId, availableQuantity));
        }
    }

    private void ValidateOrderStatus(Status status)
    {
        if (!Enum.IsDefined(status))
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.StatusNotFound, status));
        }
    }

    private void ValidateOrderStatusChanging(Status currentStatus, Status newStatus)
    {
        switch (currentStatus)
        {
            case Status.InReview:
                if (newStatus == Status.InDelivery) return;
                break;
            case Status.InDelivery:
                if (newStatus == Status.Completed) return;
                break;
        }

        throw new IncorrectParamsException(
            string.Format(ExceptionMessages.IncorrectStatusChanging, Enum.GetName(currentStatus), Enum.GetName(newStatus)));
    }

    private void ValidateProducts(List<int> productsIds, List<int> existingProductsIds)
    {
        var missingProductIds = productsIds.Except(existingProductsIds).ToList();
        if (missingProductIds.Count > 0)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, missingProductIds[0]));
        }
    }
}