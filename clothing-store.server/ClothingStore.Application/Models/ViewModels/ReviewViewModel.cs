namespace ClothingStore.Application.Models.ViewModels;

public class ReviewViewModel
{
    public int ID { get; set; }
    public string ReviewTitle { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime AddDate { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
}