namespace ClothingStore.Entities;

public class OrderProduct
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    
    public int ProductID { get; set; }
    public virtual Product Product { get; set; }
    
    public int OrderID { get; set; }
    public virtual CustomerOrder Order { get; set; }
}