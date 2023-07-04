using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Category, CategoryInputModel>().ReverseMap();
    }
}