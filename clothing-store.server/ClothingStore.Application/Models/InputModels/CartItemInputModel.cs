using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class CartItemInputModel
{
    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(1, 100000)]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "The ProductID field is required.")]
    public int ProductID { get; set; } 
    
    [Required(ErrorMessage = "The UserID field is required.")]
    public int UserID { get; set; } 
}