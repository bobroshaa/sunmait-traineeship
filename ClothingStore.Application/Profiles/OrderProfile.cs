using AutoMapper;
using ClothingStore.Application.Models;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CustomerOrder, OrderViewModel>().ReverseMap();
        CreateMap<CustomerOrder, OrderInputModel>().ReverseMap();
    }
}