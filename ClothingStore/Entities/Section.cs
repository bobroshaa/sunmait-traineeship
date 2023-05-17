namespace ClothingStore.Entities;

public class Section
{
    public int ID { get; set; }
    public string Name { get; set; } 
    
    public virtual ICollection<SectionCategory> SectionCategories { get; set; }
}