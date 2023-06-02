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
        await ValidateOrder(orderId);
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
        await ValidateUser(order.UserID);
        var productsIds = orderInputModel.Products.Select(p => p.ProductID).ToList();
        var productsDictionary = await _productRepository.GetProductsByIds(productsIds);
        ValidateProducts(productsIds, productsDictionary.Keys.ToList());
        order.OrderProducts = new List<OrderProduct>();
        foreach (var item in orderInputModel.Products)
        {
            if (productsDictionary.TryGetValue(item.ProductID, out var productToUpdate))
            {
                ValidateProductQuantity(item.ProductID, item.Quantity, productToUpdate.Quantity);
                var mappedItem = _mapper.Map<OrderProduct>(item);
                mappedItem.Price = productToUpdate.Price;
                mappedItem.OrderID = order.ID;
                order.OrderProducts.Add(mappedItem);
                productToUpdate.Quantity -= item.Quantity;
            }
            else
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound,
                    item.ProductID));
            }
        }

        _orderRepository.Add(order);
        await _orderRepository.Save();
        return order.ID;
    }

    public async Task Update(int id, Status orderStatus)
    {
        var order = await GetOrderById(id);
        ValidateOrderStatus(orderStatus);
        ValidateOrderStatusChanging(order.CurrentStatus, orderStatus);
        _orderRepository.Update(order, orderStatus);
        await _orderRepository.Save();
    }

    public async Task Delete(int id)
    {
        var order = await GetOrderById(id);
        _orderRepository.Delete(order);
        await _orderRepository.Save();
    }

    public async Task<List<OrderHistoryViewModel>> GetOrderHistoryByOrderId(int orderId)
    {
        await ValidateOrder(orderId);
        return _mapper.Map<List<OrderHistoryViewModel>>(await _orderRepository.GetOrderHistoryByOrderId(orderId));
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
            throw new IncorrectParamsException(string.Format(ExceptionMessages.ProductQuantityIsNotAvailable,
                requiredQuantity, productId, availableQuantity));
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
        if (newStatus - currentStatus != 1)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.IncorrectStatusChanging,
                Enum.GetName(currentStatus), Enum.GetName(newStatus)));
        }
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