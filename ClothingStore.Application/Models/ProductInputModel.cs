using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models;

public class ProductInputModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string ImageURL { get; set; }
    public int SectionCategoryID { get; set; }
    public int BrandID { get; set; }
}