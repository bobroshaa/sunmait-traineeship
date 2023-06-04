using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class BrandInputModel
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(50, ErrorMessage = "The Name field must not exceed 50 characters.")]
    public string Name { get; set; }
}