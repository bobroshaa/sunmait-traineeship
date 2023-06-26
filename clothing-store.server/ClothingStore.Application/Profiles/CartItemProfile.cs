﻿using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class CartItemProfile: Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem, CartItemViewModel>()
            .ForMember(ci => ci.Price, opt => opt.MapFrom(ci => ci.Product.Price))
            .ForMember(ci => ci.ImageURL, opt => opt.MapFrom(ci => ci.Product.ImageURL))
            .ForMember(ci => ci.Name, opt => opt.MapFrom(ci => ci.Product.Name))
            .ReverseMap();
        CreateMap<CartItem, CartItemInputModel>().ReverseMap();
    }
}