namespace ClothingAppDB.Entities;

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int ParentCategoryID { get; set; }

    public virtual Category ParentCategory { get; set; }
}