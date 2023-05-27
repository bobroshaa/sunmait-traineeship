using AutoMapper;
using ClothingStore.Application.Models;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderProduct, OrderItemViewModel>().ReverseMap();
        CreateMap<OrderProduct, OrderItemInputModel>().ReverseMap();
    }
}