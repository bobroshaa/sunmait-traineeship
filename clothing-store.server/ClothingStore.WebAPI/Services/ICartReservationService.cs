namespace ClothingStore.WebAPI.Services;

public interface ICartReservationService
{
    Task DeleteExpiredCartItems();
}