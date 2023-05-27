using AutoMapper;
using ClothingStore.Application.Models;
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