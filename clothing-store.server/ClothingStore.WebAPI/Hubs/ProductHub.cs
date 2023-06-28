using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace ClothingStore.WebAPI.Hubs;

public class ProductHub : Hub
{
    private static readonly ConcurrentDictionary<int, int> _viewingUsers = new();

    public async Task JoinRoomFromProduct(int productId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, productId.ToString());
        _viewingUsers[productId] = _viewingUsers.TryGetValue(productId, out var value) ? ++value : 1;
        await BroadcastToGroup(productId);
    }

    public async Task LeaveRoomFromProduct(int productId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, productId.ToString());
        _viewingUsers[productId] -= 1;
        await BroadcastToGroup(productId);
    }
    
    public async Task JoinRoomFromCart(int productId, int userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, productId.ToString());
        await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
    }

    public async Task LeaveRoomFromCart(int productId, int userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, productId.ToString());
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());
    }
    
    private async Task BroadcastToGroup(int productId)
    {
        await Clients.Group(productId.ToString())
            .SendAsync("updateViewers", _viewingUsers[productId]);
    }
}