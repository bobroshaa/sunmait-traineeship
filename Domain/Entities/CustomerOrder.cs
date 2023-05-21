using Domain.Enums;

namespace Domain.Entities;

public class CustomerOrder
{
    public int ID {get; set;}
    public DateTime OrderDate {get; set;}
    public Status CurrentStatus { get; set; }
    
    public int UserID {get; set;}
    public virtual UserAccount User {get; set;}
    
    public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}