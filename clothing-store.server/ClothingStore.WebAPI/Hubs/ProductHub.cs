using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace ClothingStore.WebAPI.Hubs;

public class ProductHub : Hub
{
    private static readonly ConcurrentDictionary<int, ConcurrentDictionary<int, int>> _viewingUsers = new();

    public async Task JoinRoomFromProduct(int productId, int userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, productId.ToString());

        if (_viewingUsers.TryGetValue(productId, out var userDictionary))
        {
            userDictionary[userId] = !userDictionary.TryGetValue(userId, out var count) ? 1 : ++count;
        }
        else
        {
            _viewingUsers[productId] = new ConcurrentDictionary<int, int> { [userId] = 1 };
        }

        await BroadcastToGroup(productId);
    }

    public async Task LeaveRoomFromProduct(int productId, int userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, productId.ToString());
        _viewingUsers[productId][userId] -= 1;

        if (_viewingUsers.TryGetValue(productId, out var productDictionary) && productDictionary.TryGetValue(userId, out var viewerCount))
        {
            if (viewerCount == 0)
            {
                productDictionary.TryRemove(userId, out _);
            }
        }
        
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
            .SendAsync("updateViewers", _viewingUsers[productId].Count);
    }
}