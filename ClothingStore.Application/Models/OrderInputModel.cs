using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models;

public class OrderInputModel
{
    public Status CurrentStatus { get; set; }
    public int UserID {get; set;}
}