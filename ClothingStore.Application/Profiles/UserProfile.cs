using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAccount, UserViewModel>().ReverseMap();
        CreateMap<UserAccount, UserInputModel>().ReverseMap();
    }
}