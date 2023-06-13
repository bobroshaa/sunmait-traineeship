using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.WebAPI.Services;

public interface IJwtGenerator
{
    string CreateToken(UserViewModel user);
}