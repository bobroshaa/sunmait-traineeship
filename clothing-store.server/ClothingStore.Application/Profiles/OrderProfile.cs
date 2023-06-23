using AutoMapper;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CustomerOrder, OrderViewModel>()
            .ForMember(d => d.CurrentStatus, opt => opt.MapFrom(o => Enum.GetName(o.CurrentStatus)))
            .ForMember(d => d.CurrentStatusId, opt => opt.MapFrom(o => o.CurrentStatus))
            .ReverseMap();
        CreateMap<CustomerOrder, OrderInputModel>().ReverseMap();
    }
}