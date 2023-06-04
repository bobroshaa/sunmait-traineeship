using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class ProductInputModel
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name field must not exceed 100 characters.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The Description field is required.")]
    [StringLength(500, ErrorMessage = "The Description field must not exceed 500 characters.")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "The Price field is required.")]
    [Range(0, 100000)]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(0, 100000)]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "The ImageURL field is required.")]
    [StringLength(500, ErrorMessage = "The ImageURL field must not exceed 500 characters.")]
    public string ImageURL { get; set; }
    
    [Required(ErrorMessage = "The SectionCategoryID field is required.")]
    public int SectionCategoryID { get; set; }
    
    public int? BrandID { get; set; }
}