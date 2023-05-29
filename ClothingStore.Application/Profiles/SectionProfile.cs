using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class SectionProfile: Profile
{
    public SectionProfile()
    {
        CreateMap<Section, SectionViewModel>().ReverseMap();
        CreateMap<Section, SectionInputModel>().ReverseMap();
    }
}