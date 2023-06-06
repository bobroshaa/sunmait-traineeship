using AutoMapper;
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
    private static IMapper _mapper;
    private readonly OrderRepository _orderRepository;
    private readonly ProductRepository _productRepository;
    private readonly UserRepository _userRepository;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _context = DatabaseInMemoryCreator.Create();
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new OrderProfile());
                mc.AddProfile(new OrderItemProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        _orderRepository = new OrderRepository(_context);
        _productRepository = new ProductRepository(_context);
        _userRepository = new UserRepository(_context);
        _orderService = new OrderService(_mapper, _orderRepository, _productRepository, _userRepository);
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

        // Act
        var addedOrderId = (await _orderService.Add(orderInputModel)).Id;

        // Assert
        var order = await _context.CustomerOrders.FirstOrDefaultAsync(co => co.ID == addedOrderId);
        order.Should().NotBeNull();
        order.OrderDate.Should().BeCloseTo(DateTime.UtcNow, new TimeSpan(0, 0, 0, 10));
        order.CurrentStatus.Should().Be(Status.InReview);
        order.UserID.Should().Be(userId);

        var orderItems = await _context.OrderProducts.Where(op => op.OrderID == addedOrderId).ToListAsync();
        orderItems.Count.Should().Be(orderInputModel.Products.Count);

        var orderItem = orderItems[0];

        var productPrice = (await _context
            .Products
            .FirstOrDefaultAsync(p => p.ID == orderInputModel.Products[0].ProductID))
            ?.Price;
        
        orderItem.Price.Should().Be(productPrice);
        orderItem.Quantity.Should().Be(quantity);
        orderItem.OrderID.Should().Be(addedOrderId);
        orderItem.ProductID.Should().Be(productId);
    }
}