using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem, CartItemViewModel>()
            .ForMember(ci => ci.Product, opt => opt.MapFrom(ci => ci.Product))
            .ReverseMap();
        CreateMap<CartItem, CartItemInputModel>().ReverseMap();
    }
}