using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.Models.InputModels;

public class UserInputModel
{
    [StringLength(20, ErrorMessage = "The Phone field must not exceed 20 characters.")]
    public string? Phone { get; set; }

    [StringLength(100, ErrorMessage = "The Email field must not exceed 100 characters.")]
    [Required(ErrorMessage = "The Email field is required.")]
    public string Email { get; set; }

    [StringLength(100, ErrorMessage = "The Password field must be equal 100 characters.")]
    [Required(ErrorMessage = "The Password is required.")]
    public string Password { get; set; }

    [StringLength(50, ErrorMessage = "The FirstName field must be equal 32 characters.")]
    [Required(ErrorMessage = "The FirstName is required.")]
    public string FirstName { get; set; }

    [StringLength(50, ErrorMessage = "The LastName field must be equal 32 characters.")]
    [Required(ErrorMessage = "The LastName is required.")]
    public string LastName { get; set; }
}