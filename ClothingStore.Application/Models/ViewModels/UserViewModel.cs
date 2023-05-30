using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.ViewModels;

public class UserViewModel
{
    public int ID { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressViewModel? Address { get; set; }
}