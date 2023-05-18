using AutoMapper;
using ClothingStore.Entities;
using ClothingStore.Models;

namespace ClothingStore.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandViewModel>().ReverseMap();
        CreateMap<Brand, BrandInputModel>().ReverseMap();
    }
}