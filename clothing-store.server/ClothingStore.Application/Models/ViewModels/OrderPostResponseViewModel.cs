namespace ClothingStore.Application.Models.ViewModels;

public class OrderPostResponseViewModel
{
    public int OrderId { get; set; }
    public Dictionary<int, int> ProductReservedQuantity { get; set; }
}