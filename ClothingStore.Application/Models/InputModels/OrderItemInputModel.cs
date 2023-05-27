namespace ClothingStore.Application.Models.InputModels;

public class OrderItemInputModel
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
}