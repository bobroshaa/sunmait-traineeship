using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.ViewModels;

public class OrderHistoryViewModel
{
    public int ID { get; set; }
    public Status StatusId { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public int OrderID { get; set; }
}