using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductInputModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>()
            .ForMember(p => p.BrandName, opt => opt.MapFrom(p => p.Brand.Name))
            .ReverseMap();
    }
}