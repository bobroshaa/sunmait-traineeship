using System.ComponentModel.DataAnnotations;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.InputModels;

public class OrderInputModel
{
    [Required(ErrorMessage = "The UserID field is required.")]
    public int UserID {get; set;}

    [Required(ErrorMessage = "At least one cart item is required.")]
    [MinLength(1, ErrorMessage = "At least one cart item is required.")]
    public List<int> CartItemIds { get; set; }
}