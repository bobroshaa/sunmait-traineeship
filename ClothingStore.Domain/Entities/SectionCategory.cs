﻿namespace ClothingStore.Domain.Entities;

public class SectionCategory
{
    public int ID { get; set; }
    
    public int SectionID { get; set; }
    public virtual Section Section { get; set; }
    public int CategoryID { get; set; }
    
    public virtual Category Category { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}