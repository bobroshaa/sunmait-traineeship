namespace ClothingStore.Domain.Models;

public class ProductResponseModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime AddDate { get; set; }
    public int Quantity { get; set; }
    public string ImageURL { get; set; }
    public string Section { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
}