using AutoMapper;
using Domain.Entities;
using WebAPI.Models;

namespace WebAPI.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandViewModel>().ReverseMap();
        CreateMap<Brand, BrandInputModel>().ReverseMap();
    }
}