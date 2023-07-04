using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities;

public class Address
{
    public int UserID { get; set; }
    public Country Country {get; set;}
    public string District { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public bool IsActive { get; set; }

    public virtual UserAccount User { get; set; }
}