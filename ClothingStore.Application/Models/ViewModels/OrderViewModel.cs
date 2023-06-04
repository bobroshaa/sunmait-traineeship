using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.ViewModels;

public class OrderViewModel
{
    public int ID {get; set;}
    public DateTime OrderDate {get; set;}
    public Status CurrentStatusId { get; set; }
    public string CurrentStatus { get; set; }
    public int UserID {get; set;}
}