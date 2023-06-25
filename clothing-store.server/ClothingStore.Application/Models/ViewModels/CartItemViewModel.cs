using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.ViewModels;

public class CartItemViewModel
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public string Name { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
    public CartItemStatus CartItemStatus { get; set; }
}