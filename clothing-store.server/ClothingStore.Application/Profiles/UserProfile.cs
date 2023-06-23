using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAccount, UserViewModel>()
            .ForMember(uvm => uvm.Address, opt => opt.MapFrom(u => u.Address))
            .ForMember(d => d.Role, opt => opt.MapFrom(u => Enum.GetName(u.Role)))
            .ForMember(d => d.RoleId, opt => opt.MapFrom(u => u.Role))
            .ReverseMap();
        CreateMap<UserAccount, UserInputModel>().ReverseMap();
    }
}