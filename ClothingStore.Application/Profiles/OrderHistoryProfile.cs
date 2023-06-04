using AutoMapper;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class OrderHistoryProfile : Profile
{
    public OrderHistoryProfile()
    {
        CreateMap<OrderHistory, OrderHistoryViewModel>()
            .ForMember(d => d.Status, opt => opt.MapFrom(oh => Enum.GetName(oh.Status)))
            .ForMember(d => d.StatusId, opt => opt.MapFrom(oh => oh.Status))
            .ReverseMap();
    }
}