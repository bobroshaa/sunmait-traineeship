namespace ClothingStore.Application.Models;

public class ProductViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime AddDate { get; set; }
    public int Quantity { get; set; }
    public string ImageURL { get; set; }
    public string SectionName { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
}