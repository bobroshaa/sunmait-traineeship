using AutoMapper;
using ClothingStore.Models;
using Domain.Entities;

namespace ClothingStore.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandViewModel>().ReverseMap();
        CreateMap<Brand, BrandInputModel>().ReverseMap();
    }
}