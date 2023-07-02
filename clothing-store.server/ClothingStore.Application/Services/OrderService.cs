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
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IMapper mapper,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        ICartRepository cartRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _cartRepository = cartRepository;
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

    public async Task<List<CartItemViewModel>> Add(OrderInputModel orderInputModel)
    {
        var userId = orderInputModel.CartItems[0].UserID;
        await ValidateUser(userId);
        
        var order = new CustomerOrder { UserID = userId };

        var cartItemIds = orderInputModel.CartItems.Select(ci => ci.ID).ToList();
        var cartItemsDictionary = await _cartRepository.GetCartItemsByIds(cartItemIds);

        ValidateCartItems(cartItemIds, cartItemsDictionary.Keys.ToList());

        order.OrderProducts = new List<OrderProduct>();
        var deletedCartItems = new List<CartItem>();

        foreach (var cartItemId in cartItemIds)
        {
            if (cartItemsDictionary.TryGetValue(cartItemId, out var cartItem))
            {
                ValidateProductQuantity(cartItem, cartItem.Product);

                var item = new OrderProduct
                {
                    ProductID = cartItem.ProductID,
                    OrderID = order.ID,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price
                };

                order.OrderProducts.Add(item);

                cartItem.Product.ReservedQuantity -= cartItem.Quantity;
                cartItem.Product.InStockQuantity -= cartItem.Quantity;
                cartItem.IsActive = false;
                deletedCartItems.Add(cartItem);
            }
            else
            {
                throw new EntityNotFoundException(
                    string.Format(ExceptionMessages.CartItemNotFound, cartItem.ProductID));
            }
        }

        order.CurrentStatus = Status.InReview;

        _orderRepository.Add(order);

        await _orderRepository.SaveChanges();

        var deletedCarItemVms = _mapper.Map<List<CartItemViewModel>>(deletedCartItems);

        return deletedCarItemVms;
    }

    public async Task Update(int id, Status orderStatus)
    {
        var order = await GetOrderById(id);

        ValidateOrderStatus(orderStatus);
        ValidateOrderStatusChanging(order.CurrentStatus, orderStatus);

        _orderRepository.Update(order, orderStatus);

        await _orderRepository.SaveChanges();
    }

    public async Task<List<ProductViewModel>> Delete(int id)
    {
        var order = await GetOrderById(id);
        var orderProducts = await GetOrderItemsByOrderId(id);
        var orderProductsDictionary = orderProducts.ToDictionary(item => item.ProductID, item => item);

        var products = await _productRepository.GetProductsByIds(orderProductsDictionary.Keys.ToList());

        _orderRepository.Delete(order);

        foreach (var product in products)
        {
            product.Value.InStockQuantity += orderProductsDictionary[product.Key].Quantity;
        }

        var productVms = _mapper.Map<List<ProductViewModel>>(products.Values);

        await _orderRepository.SaveChanges();
        return productVms;
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

    private void ValidateProductQuantity(CartItem cartItem,Product product)
    {
        if (cartItem.Quantity > product.InStockQuantity)
        {
            throw new IncorrectParamsException(string.Format(
                    ExceptionMessages.ProductQuantityIsNotAvailable,
                    cartItem.Quantity,
                    product.ID,
                    product.InStockQuantity));
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
            string.Format(ExceptionMessages.IncorrectStatusChanging, Enum.GetName(currentStatus),
                Enum.GetName(newStatus)));
    }

    private void ValidateCartItems(List<int> cartItemIds, List<int> existingCartItems)
    {
        var missingCartItemIds = cartItemIds.Except(existingCartItems).ToList();
        if (missingCartItemIds.Count > 0)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CartItemNotFound, missingCartItemIds[0]));
        }
    }
}