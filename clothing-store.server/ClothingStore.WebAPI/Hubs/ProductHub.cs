using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace ClothingStore.WebAPI.Hubs;

public class ProductHub : Hub
{
    private static readonly ConcurrentDictionary<int, int> _viewingUsers = new();

    public async Task JoinRoom(int productId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, productId.ToString());
        _viewingUsers[productId] = _viewingUsers.TryGetValue(productId, out var value) ? ++value : 1;
        await BroadcastToGroup(productId);
    }

    public async Task LeaveRoom(int productId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, productId.ToString());
        _viewingUsers[productId] -= 1;
        await BroadcastToGroup(productId);
    }

    private async Task BroadcastToGroup(int productId)
    {
        await Clients.Group(productId.ToString())
            .SendAsync("broadcasttogroup", _viewingUsers[productId]);
    }
}