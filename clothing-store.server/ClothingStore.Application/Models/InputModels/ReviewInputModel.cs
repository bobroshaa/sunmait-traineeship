using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class ReviewInputModel
{
    [Required(ErrorMessage = "The ReviewTitle field is required.")]
    [StringLength(50, ErrorMessage = "The ReviewTitle field must not exceed 50 characters.")]
    public string ReviewTitle { get; set; }
    
    [Required(ErrorMessage = "The Comment field is required.")]
    [StringLength(500, ErrorMessage = "The Comment field must not exceed 500 characters.")]
    public string Comment { get; set; }
    
    [Range(0, 5, ErrorMessage = "Value for Rating field must be between 0 and 5.")]
    [Required(ErrorMessage = "The Rating field is required.")]
    public int Rating { get; set; }
    
    [Required(ErrorMessage = "The ProductID field is required.")]
    public int ProductID { get; set; }
    
    [Required(ErrorMessage = "The UserID field is required.")]
    public int UserID { get; set; }
}