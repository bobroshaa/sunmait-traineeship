namespace ClothingStore.Application.Models.ViewModels;

public class CategoryViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryID { get; set; }
}