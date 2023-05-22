using AutoMapper;
using ClothingStore.Domain.Entities;
using ClothingStore.WebAPI.Models;

namespace ClothingStore.WebAPI.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandViewModel>().ReverseMap();
        CreateMap<Brand, BrandInputModel>().ReverseMap();
    }
}