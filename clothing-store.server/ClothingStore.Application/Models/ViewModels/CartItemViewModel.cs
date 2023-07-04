using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.ViewModels;

public class CartItemViewModel
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
    public int Quantity { get; set; }
    public ProductViewModel Product { get; set; }
}