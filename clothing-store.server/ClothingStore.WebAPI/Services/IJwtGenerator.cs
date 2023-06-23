using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.WebAPI.Services;

public interface IJwtGenerator
{
    TokenViewModel CreateToken(UserViewModel user);
}