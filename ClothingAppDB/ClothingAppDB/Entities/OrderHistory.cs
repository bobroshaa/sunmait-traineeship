namespace ClothingAppDB.Entities;

public class OrderHistory
{
    public int ID { get; set; }
    public int OrderID { get; set; }
    //public Status Status { get; set; }
    public DateTime Date { get; set; } 
}