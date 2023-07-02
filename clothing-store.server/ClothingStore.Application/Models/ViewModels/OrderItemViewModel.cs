namespace ClothingStore.Application.Models.ViewModels;

public class OrderItemViewModel
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
}