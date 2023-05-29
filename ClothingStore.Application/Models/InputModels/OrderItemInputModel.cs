using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class OrderItemInputModel
{
    [Required(ErrorMessage = "The Quantity field is required.")]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "The Price field is required.")]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "The ProductID field is required.")]
    public int ProductID { get; set; }
}