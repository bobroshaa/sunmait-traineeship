using System.ComponentModel.DataAnnotations;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.InputModels;

public class OrderInputModel
{
    [Required(ErrorMessage = "At least one cart item is required.")]
    [MinLength(1, ErrorMessage = "At least one cart item is required.")]
    public List<CartItemViewModel> CartItems { get; set; }
}