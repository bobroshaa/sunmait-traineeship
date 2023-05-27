namespace ClothingStore.Application.Models;

public class OrderItemViewModel
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
}