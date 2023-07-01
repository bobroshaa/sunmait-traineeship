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

    public async Task DeleteExpiredCartItems()
    {
        var productIdsOfDeletedItems = await _cartService.DeleteExpiredCartItems();
        if (!productIdsOfDeletedItems.IsNullOrEmpty())
        {
            foreach (var cartItem in productIdsOfDeletedItems)
            {
                await UpdateReservedQuantity(cartItem.ProductID, cartItem.ReservedQuantity);
            }
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