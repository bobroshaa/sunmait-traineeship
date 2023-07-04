namespace ClothingStore.WebAPI.Services;

public interface ISignalRService
{
    Task DeleteExpiredCartItem(int id);
    Task UpdateReservedQuantity(int productId, int reservedQuantity);
    Task UpdateInStockQuantity(int productId, int inStockQuantity);
    Task UpdateCart(int userId);
    Task UpdateCartItemQuantity(int userId, int cartItemId, int quantity);
}