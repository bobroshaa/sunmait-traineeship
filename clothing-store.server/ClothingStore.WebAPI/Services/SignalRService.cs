using ClothingStore.Application.Interfaces;
using ClothingStore.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

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
            await UpdateReservedQuantity(cartItem.ProductID, cartItem.ReservedQuantity);
            await UpdateCart(cartItem.UserID);
        }
    }

    public async Task UpdateReservedQuantity(int productId, int reservedQuantity)
    {
        await _productHub
            .Clients
            .Group(productId.ToString())
            .SendAsync("updateReservedQuantity", reservedQuantity, productId);
    }
    
    public async Task UpdateInStockQuantity(int productId, int inStockQuantity)
    {
        await _productHub
            .Clients
            .Group(productId.ToString())
            .SendAsync("updateInStockQuantity", inStockQuantity, productId);
    }


    public async Task UpdateCart(int userId)
    {
        await _productHub
            .Clients
            .Group(userId.ToString())
            .SendAsync("updateCart");
    }
    
    public async Task UpdateCartItemQuantity(int userId, int cartItemId, int quantity)
    {
        await _productHub
            .Clients
            .Group(userId.ToString())
            .SendAsync("updateCartItemQuantity", cartItemId, quantity);
    }
    
}