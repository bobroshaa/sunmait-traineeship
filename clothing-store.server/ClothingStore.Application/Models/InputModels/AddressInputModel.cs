using System.ComponentModel.DataAnnotations;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Models.InputModels;

public class AddressInputModel
{
    [Required(ErrorMessage = "The Country field is required.")]
    public Country Country {get; set;}
    
    [Required(ErrorMessage = "The District field is required.")]
    [StringLength(50, ErrorMessage = "The District field must not exceed 50 characters.")]
    public string District { get; set; }
    
    [Required(ErrorMessage = "The City field is required.")]
    [StringLength(50, ErrorMessage = "The City field must not exceed 50 characters.")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "The Postcode field is required.")]
    [StringLength(10, ErrorMessage = "The Postcode field must not exceed 10 characters.")]
    public string Postcode { get; set; }
    
    [Required(ErrorMessage = "The AddressLine1 field is required.")]
    [StringLength(50, ErrorMessage = "The AddressLine1 field must not exceed 50 characters.")]
    public string AddressLine1 { get; set; }
    
    [Required(ErrorMessage = "The AddressLine2 field is required.")]
    [StringLength(20, ErrorMessage = "The AddressLine2 field must not exceed 20 characters.")]
    public string AddressLine2 { get; set; }
}