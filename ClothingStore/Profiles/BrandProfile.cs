using AutoMapper;
using ClothingStore.Entities;
using ClothingStore.ViewModels;

namespace ClothingStore;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandViewModel>().ReverseMap();
    }
}