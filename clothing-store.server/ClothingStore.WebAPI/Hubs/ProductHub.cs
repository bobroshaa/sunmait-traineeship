﻿using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace ClothingStore.WebAPI.Hubs;

public class ProductHub : Hub
{
    private static readonly ConcurrentDictionary<int, ConcurrentDictionary<int, int>> ViewingUsers = new();

    public async Task JoinRoomFromProduct(int productId, int userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "product" + productId);

        if (ViewingUsers.TryGetValue(productId, out var userDictionary))
        {
            userDictionary[userId] = !userDictionary.TryGetValue(userId, out var count) ? 1 : ++count;
        }
        else
        {
            ViewingUsers[productId] = new ConcurrentDictionary<int, int> { [userId] = 1 };
        }

        await BroadcastToGroup(productId);
    }

    public async Task LeaveRoomFromProduct(int productId, int userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "product" + productId);
        ViewingUsers[productId][userId] -= 1;

        if (ViewingUsers.TryGetValue(productId, out var productDictionary) &&
            productDictionary.TryGetValue(userId, out var viewerCount))
        {
            if (viewerCount == 0)
            {
                productDictionary.TryRemove(userId, out _);
            }
        }

        await BroadcastToGroup(productId);
    }

    public async Task JoinRoomFromCart(int[] productIds, int userId)
    {
        foreach (var id in productIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "product" + id);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, "cart" + userId);
    }

    public async Task LeaveRoomFromCart(int[] productIds, int userId)
    {
        foreach (var id in productIds)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "product" + id);
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "cart" + userId);
    }

    private async Task BroadcastToGroup(int productId)
    {
        await Clients.Group("product" + productId)
            .SendAsync("updateViewers", ViewingUsers[productId].Count);
    }
}