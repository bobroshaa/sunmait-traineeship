namespace ClothingAppDB.Entities;

public class OrderProduct
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public int Qauntity { get; set; }
    public double Price { get; set; }
    
    public virtual Product Product { get; set; }
    public virtual CustomerOrder Order { get; set; }
}