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
        });
        IMapper mapper = mappingConfig.CreateMapper();

        var orderRepository = new OrderRepository(_context);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        _orderService = new OrderService(mapper, orderRepository, productRepository, userRepository);
    }

    [Theory]
    [InlineData(1, 1, 2)]
    public async Task AddNewOrder_ValidValues_Success(int userId, int productId, int quantity)
    {
        // Arrange
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

    [Theory]
    [InlineData(10, 1, 2)]
    public async Task AddNewOrder_InvalidUserId_Failure(int userId, int productId, int quantity)
    {
        // Arrange
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

    [Theory]
    [InlineData(1, 10, 2)]
    public async Task AddNewOrder_InvalidProductId_Failure(int userId, int productId, int quantity)
    {
        // Arrange
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

    [Theory]
    [InlineData(1, 1, 200)]
    public async Task AddNewOrder_UnavailableProductQuantity_Failure(int userId, int productId, int quantity)
    {
        // Arrange
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
}