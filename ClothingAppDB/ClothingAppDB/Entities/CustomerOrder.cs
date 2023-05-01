namespace ClothingAppDB.Entities;

public class CustomerOrder
{
    public int ID {get; set;}
    public int UserID {get; set;}
    public DateTime OrderDate {get; set;}
    //public Status CurrentStatus { get; set; }
    
    public virtual UserAccount User {get; set;}
}