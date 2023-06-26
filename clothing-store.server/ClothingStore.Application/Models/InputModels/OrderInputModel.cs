using System.ComponentModel.DataAnnotations;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.InputModels;

public class OrderInputModel
{
    public OrderInputModel()
    {
        CartItemIds = new List<int>();
    }
    
    [Required(ErrorMessage = "The UserID field is required.")]
    public int UserID {get; set;}

    public List<int> CartItemIds { get; set; }
}