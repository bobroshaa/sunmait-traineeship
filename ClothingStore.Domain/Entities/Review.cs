namespace ClothingStore.Domain.Entities;

public class Review
{
    public int ID { get; set; }
    public string ReviewTitle { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime AddDate { get; set; }
    
    public int ProductID { get; set; }
    public virtual Product Product { get; set; }
    
    public int UserID { get; set; }
    public virtual UserAccount User { get; set; }
}