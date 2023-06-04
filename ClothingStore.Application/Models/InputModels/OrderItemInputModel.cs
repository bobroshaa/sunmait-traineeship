using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class OrderItemInputModel
{
    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(0, 100000)]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "The ProductID field is required.")]
    public int ProductID { get; set; }
}