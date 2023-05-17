namespace ClothingStore.Entities;

public class Brand
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<Product> Products { get; set; }
}