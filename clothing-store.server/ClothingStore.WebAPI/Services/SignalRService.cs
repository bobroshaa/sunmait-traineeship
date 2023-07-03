using ClothingStore.Application.Interfaces;
using ClothingStore.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ClothingStore.WebAPI.Services;

public class SignalRService : ISignalRService
{
    private readonly ICartService _cartService;
    private readonly IHubContext<ProductHub> _productHub;

    public SignalRService(ICartService cartService, IHubContext<ProductHub> productHub)
    {
        _cartService = cartService;
        _productHub = productHub;
    }

    public async Task DeleteExpiredCartItem(int id)
    {
        var cartItem = await _cartService.DeleteExpiredCartItem(id);
        if (cartItem is not null)
        {
            await UpdateReservedQuantity(cartItem.ProductID, cartItem.Product.ReservedQuantity);
            await UpdateCart(cartItem.UserID);
        }
    }

    public async Task UpdateReservedQuantity(int productId, int reservedQuantity)
    {
        await _productHub
            .Clients
            .Group("product" + productId)
            .SendAsync("updateReservedQuantity", reservedQuantity, productId);
    }
    
    public async Task UpdateInStockQuantity(int productId, int inStockQuantity)
    {
        await _productHub
            .Clients
            .Group("product" + productId)
            .SendAsync("updateInStockQuantity", inStockQuantity, productId);
    }


    public async Task UpdateCart(int userId)
    {
        await _productHub
            .Clients
            .Group("cart" + userId)
            .SendAsync("updateCart");
    }
    
    public async Task UpdateCartItemQuantity(int userId, int cartItemId, int quantity)
    {
        await _productHub
            .Clients
            .Group("cart" + userId)
            .SendAsync("updateCartItemQuantity", cartItemId, quantity);
    }
    
}