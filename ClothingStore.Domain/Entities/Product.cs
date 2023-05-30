namespace ClothingStore.Domain.Entities;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime AddDate { get; set; }
    public int Quantity { get; set; }
    public string ImageURL { get; set; }
    public bool IsActive { get; set; }
    
    public int SectionCategoryID { get; set; }
    public virtual SectionCategory SectionCategory { get; set; }
    public int? BrandID { get; set; }
    public virtual Brand Brand { get; set; }
    
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
}