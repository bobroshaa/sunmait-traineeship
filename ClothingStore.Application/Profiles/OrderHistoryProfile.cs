using AutoMapper;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class OrderHistoryProfile: Profile
{
    public OrderHistoryProfile()
    {
        CreateMap<OrderHistory, OrderHistoryViewModel>().ReverseMap();
    }
}