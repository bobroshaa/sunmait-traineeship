using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.ViewModels;

public class AddressViewModel
{
    public int UserID { get; set; }
    public Country CountryId {get; set;}
    public string Country {get; set;}
    public string District { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
}