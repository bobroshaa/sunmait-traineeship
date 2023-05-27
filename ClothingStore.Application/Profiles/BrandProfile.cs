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
        
        CreateMap<Product, ProductInputModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>()
            .ForMember(pvm => pvm.SectionName, opt => opt.MapFrom(p => p.SectionCategory.Section.Name))
            .ForMember(pvm => pvm.CategoryName, opt => opt.MapFrom(p => p.SectionCategory.Category.Name))
            .ForMember(pvm => pvm.BrandName, opt => opt.MapFrom(p => p.Brand.Name))
            .ReverseMap();
        
        CreateMap<CustomerOrder, OrderViewModel>().ReverseMap();
        CreateMap<CustomerOrder, OrderInputModel>().ReverseMap();

    }
}