using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressViewModel>()
            .ForMember(d => d.Country, opt => opt.MapFrom(a => Enum.GetName(a.Country)))
            .ForMember(d => d.CountryId, opt => opt.MapFrom(a => a.Country))
            .ReverseMap();
        CreateMap<Address, AddressInputModel>().ReverseMap();
    }
}