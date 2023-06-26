using ClothingStore.Application.Interfaces;
using ClothingStore.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace ClothingStore.WebAPI.Services;

public class CartReservationService : ICartReservationService
{
    private readonly ICartService _cartService;
    private readonly IHubContext<ProductHub> _productHub;

    public CartReservationService(ICartService cartService, IHubContext<ProductHub> productHub)
    {
        _cartService = cartService;
        _productHub = productHub;
    }

    public async Task DeleteExpiredCartItems()
    {
        var productIdsOfDeletedItems = await _cartService.DeleteExpiredCartItems();
        if (!productIdsOfDeletedItems.IsNullOrEmpty())
        {
            foreach (var item in productIdsOfDeletedItems)
            {
                await _productHub.Clients.Group(item.Key.ToString()).SendAsync("updateReserved", item.Value);
            }
        }
    }
}