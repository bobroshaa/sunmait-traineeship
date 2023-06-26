namespace ClothingStore.Application.Models.ViewModels;

public class ProductViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime AddDate { get; set; }
    public int InStockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public string ImageURL { get; set; }
    public int SectionCategoryId { get; set; }
    public int? BrandId { get; set; }
}