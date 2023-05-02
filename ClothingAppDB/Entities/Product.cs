using ClothingAppDB.Entities.Enums;

namespace ClothingAppDB.Entities;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime AddDate { get; set; }
    public int Quantity { get; set; }
    public string ImageURL { get; set; }
    public Sex Sex { get; set; }
    
    public int CategoryID { get; set; }
    public virtual Category Category { get; set; }
    
    public int SectionID { get; set; }
    public virtual Section Section { get; set; }
    
    public int BrandID { get; set; }
    public virtual Brand Brand { get; set; }
    
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
}