using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class BrandViewModel
{
    public int ID { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(50, ErrorMessage = "The Name field must not exceed 50 characters.")]
    public string Name { get; set; }
}