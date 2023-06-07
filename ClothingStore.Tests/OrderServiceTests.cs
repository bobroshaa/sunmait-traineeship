using AutoMapper;
using ClothingStore.Application;
using ClothingStore.Application.Exceptions;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Profiles;
using ClothingStore.Application.Services;
using ClothingStore.Domain.Enums;
using ClothingStore.Infrastructure.Data;
using ClothingStore.Infrastructure.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClothingStore.Tests;

public class OrderServiceTests
{
    private readonly Context _context;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _context = DatabaseInMemoryCreator.Create();

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new OrderProfile());
            mc.AddProfile(new OrderItemProfile());
            mc.AddProfile(new OrderHistoryProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();

        var orderRepository = new OrderRepository(_context);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        _orderService = new OrderService(mapper, orderRepository, productRepository, userRepository);
    }

    [Fact]
    public async Task AddNewOrder_ValidValues_Success()
    {
        // Arrange
        const int userId = 1;
        const int productId = 1;
        const int quantity = 2;

        var orderInputModel = new OrderInputModel
        {
            UserID = userId,
            Products = new List<OrderItemInputModel>
            {
                new()
                {
                    ProductID = productId,
                    Quantity = quantity
                }
            }
        };
        var product = await _context
            .Products
            .FirstOrDefaultAsync(p => p.ID == orderInputModel.Products[0].ProductID);
        var startProductQuantity = product?.Quantity;

        // Act
        var addedOrderId = (await _orderService.Add(orderInputModel)).Id;

        // Assert
        var order = await _context.CustomerOrders.FirstOrDefaultAsync(co => co.ID == addedOrderId);
        order.Should().NotBeNull();
        order.OrderDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
        order.CurrentStatus.Should().Be(Status.InReview);
        order.UserID.Should().Be(userId);

        var orderItems = await _context.OrderProducts.Where(op => op.OrderID == addedOrderId).ToListAsync();
        orderItems.Count.Should().Be(orderInputModel.Products.Count);

        var orderItem = orderItems[0];

        var productPrice = product?.Price;

        orderItem.Price.Should().Be(productPrice);
        orderItem.Quantity.Should().Be(quantity);
        orderItem.OrderID.Should().Be(addedOrderId);
        orderItem.ProductID.Should().Be(productId);

        product?.Quantity.Should().Be(startProductQuantity - quantity);
    }

    [Fact]
    public async Task AddNewOrder_InvalidUserId_Failure()
    {
        // Arrange
        const int userId = 10;
        const int productId = 1;
        const int quantity = 2;

        var orderInputModel = new OrderInputModel
        {
            UserID = userId,
            Products = new List<OrderItemInputModel>
            {
                new()
                {
                    ProductID = productId,
                    Quantity = quantity
                }
            }
        };

        // Act
        Func<Task> action = async () => await _orderService.Add(orderInputModel);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityNotFoundException>()
            .WithMessage(string.Format(ExceptionMessages.UserNotFound, userId));
    }

    [Fact]
    public async Task AddNewOrder_InvalidProductId_Failure()
    {
        // Arrange
        const int userId = 1;
        const int productId = 10;
        const int quantity = 2;

        var orderInputModel = new OrderInputModel
        {
            UserID = userId,
            Products = new List<OrderItemInputModel>
            {
                new()
                {
                    ProductID = productId,
                    Quantity = quantity
                }
            }
        };

        // Act
        Func<Task> action = async () => await _orderService.Add(orderInputModel);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityNotFoundException>()
            .WithMessage(string.Format(ExceptionMessages.ProductNotFound, productId));
    }

    [Fact]
    public async Task AddNewOrder_UnavailableProductQuantity_Failure()
    {
        // Arrange
        const int userId = 1;
        const int productId = 1;
        const int quantity = 2000;

        var orderInputModel = new OrderInputModel
        {
            UserID = userId,
            Products = new List<OrderItemInputModel>
            {
                new()
                {
                    ProductID = productId,
                    Quantity = quantity
                }
            }
        };

        // Act
        Func<Task> action = async () => await _orderService.Add(orderInputModel);

        // Assert
        var availableQuantity = (await _context.Products.FirstOrDefaultAsync(p => p.ID == productId))?.Quantity;

        await action
            .Should()
            .ThrowAsync<IncorrectParamsException>()
            .WithMessage(
                string.Format(ExceptionMessages.ProductQuantityIsNotAvailable, quantity, productId, availableQuantity));
    }

    [Fact]
    public async Task UpdateStatus_FromInReview_ValidStatus_Success()
    {
        // Arrange
        const int orderId = 1;
        const Status status = Status.InDelivery;

        // Act
        await _orderService.Update(orderId, status);

        // Assert
        (await _context.CustomerOrders.FirstOrDefaultAsync(o => o.ID == orderId)).CurrentStatus.Should().Be(status);
    }

    [Fact]
    public async Task UpdateStatus_FromInDelivery_ValidStatus_Success()
    {
        // Arrange
        const int orderId = 1;
        const Status status = Status.Completed;
        (await _context.CustomerOrders.FirstOrDefaultAsync(o => o.ID == orderId)).CurrentStatus = Status.InDelivery;

        // Act
        await _orderService.Update(orderId, status);

        // Assert
        (await _context.CustomerOrders.FirstOrDefaultAsync(o => o.ID == orderId)).CurrentStatus.Should().Be(status);
    }

    [Theory]
    [InlineData(Status.InReview)]
    [InlineData(Status.Completed)]
    public async Task UpdateStatus_FromInReview_InvalidStatus_Failure(Status status)
    {
        // Arrange
        const int orderId = 1;

        // Act
        Func<Task> action = async () => await _orderService.Update(orderId, status);

        // Assert
        await action
            .Should()
            .ThrowAsync<IncorrectParamsException>()
            .WithMessage(
                string.Format(ExceptionMessages.IncorrectStatusChanging, Enum.GetName(Status.InReview), Enum.GetName(status)));
    }

    [Theory]
    [InlineData(Status.InDelivery)]
    [InlineData(Status.InReview)]
    public async Task UpdateStatus_FromInDelivery_InvalidStatus_Failure(Status status)
    {
        // Arrange
        const int orderId = 1;
        const Status startStatus = Status.InDelivery;
        (await _context.CustomerOrders.FirstOrDefaultAsync(o => o.ID == orderId)).CurrentStatus = startStatus;

        // Act
        Func<Task> action = async () => await _orderService.Update(orderId, status);

        // Assert
        await action
            .Should()
            .ThrowAsync<IncorrectParamsException>()
            .WithMessage(
                string.Format(ExceptionMessages.IncorrectStatusChanging, Enum.GetName(startStatus), Enum.GetName(status)));
    }

    [Theory]
    [InlineData(Status.InReview)]
    [InlineData(Status.InDelivery)]
    [InlineData(Status.Completed)]
    public async Task UpdateStatus_FromInCompleted_InvalidStatus_Failure(Status status)
    {
        // Arrange
        const int orderId = 1;
        const Status startStatus = Status.Completed;
        (await _context.CustomerOrders.FirstOrDefaultAsync(o => o.ID == orderId)).CurrentStatus = startStatus;

        // Act
        Func<Task> action = async () => await _orderService.Update(orderId, status);

        // Assert
        await action
            .Should()
            .ThrowAsync<IncorrectParamsException>()
            .WithMessage(
                string.Format(ExceptionMessages.IncorrectStatusChanging, Enum.GetName(startStatus), Enum.GetName(status)));
    }

    [Theory]
    [InlineData((Status)3)]
    [InlineData((Status)10)]
    public async Task UpdateStatus_InvalidStatus_Failure(Status status)
    {
        // Arrange
        const int orderId = 1;

        // Act
        Func<Task> action = async () => await _orderService.Update(orderId, status);

        // Assert
        await action
            .Should()
            .ThrowAsync<IncorrectParamsException>()
            .WithMessage(string.Format(ExceptionMessages.StatusNotFound, status));
    }
    
    [Fact]
    public async Task GetOrderHistoryByOrderId_ValidOrderId_Success()
    {
        // Arrange
        const int orderId = 1;

        // Act
        var orderHistoryVms = await _orderService.GetOrderHistoryByOrderId(orderId);

        // Assert
        orderHistoryVms.Should().NotBeNull();
        orderHistoryVms.Count.Should().Be(2);
    }
    
    [Theory]
    [InlineData(1000)]
    [InlineData(20)]
    public async Task GetOrderHistoryByOrderId_InvalidOrderId_Failure(int orderId)
    {
        // Act
        Func<Task> action = async () => await _orderService.GetOrderHistoryByOrderId(orderId);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityNotFoundException>()
            .WithMessage(string.Format(ExceptionMessages.OrderNotFound, orderId));
    }
    
    [Fact]
    public async Task GetOrderItemsByOrderId_ValidOrderId_Success()
    {
        // Arrange
        const int orderId = 1;

        // Act
        var orderItems = await _orderService.GetOrderItemsByOrderId(orderId);

        // Assert
        orderItems.Should().NotBeNull();
        orderItems.Count.Should().Be(1);
    }
    
    [Theory]
    [InlineData(1000)]
    [InlineData(20)]
    public async Task GetOrderItemsByOrderId_InvalidOrderId_Failure(int orderId)
    {
        // Act
        Func<Task> action = async () => await _orderService.GetOrderItemsByOrderId(orderId);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityNotFoundException>()
            .WithMessage(string.Format(ExceptionMessages.OrderNotFound, orderId));
    }
}