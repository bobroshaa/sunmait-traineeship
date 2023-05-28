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
            .ForMember(pvm => pvm.SectionName, opt => opt.MapFrom(p => p.SectionCategory.Section.Name))
            .ForMember(pvm => pvm.CategoryName, opt => opt.MapFrom(p => p.SectionCategory.Category.Name))
            .ForMember(pvm => pvm.BrandName, opt => opt.MapFrom(p => p.Brand.Name))
            .ReverseMap();
    }
}