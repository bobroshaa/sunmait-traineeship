using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressViewModel>().ReverseMap();
        CreateMap<Address, AddressInputModel>().ReverseMap();
    }
}