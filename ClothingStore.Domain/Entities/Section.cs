﻿namespace ClothingStore.Domain.Entities;

public class Section
{
    public int ID { get; set; }
    public string Name { get; set; } 
    public bool IsActive { get; set; }
    
    public virtual ICollection<SectionCategory> SectionCategories { get; set; }
}