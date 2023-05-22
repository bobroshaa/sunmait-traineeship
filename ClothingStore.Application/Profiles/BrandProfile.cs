using AutoMapper;
using ClothingStore.Application.Models;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandViewModel>().ReverseMap();
        CreateMap<Brand, BrandInputModel>().ReverseMap();
    }
}