using System.ComponentModel.DataAnnotations;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.InputModels;

public class OrderInputModel
{
    [Required(ErrorMessage = "The UserID field is required.")]
    public int UserID {get; set;}

    [Required(ErrorMessage = "At least one product is required.")]
    public List<OrderItemInputModel> Products { get; set; }
}