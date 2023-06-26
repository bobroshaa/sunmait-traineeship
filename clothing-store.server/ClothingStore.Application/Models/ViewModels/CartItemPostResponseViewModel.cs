namespace ClothingStore.Application.Models.ViewModels;

public class CartItemPostResponseViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ReservationTime { get; set; }
    public int ReservedQuantity { get; set; }
}