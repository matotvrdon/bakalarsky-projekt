using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Theme;

namespace Web.Services.Mapper;

public class ThemeProfile : Profile
{
    public ThemeProfile()
    {
        CreateMap<Theme, ThemeDto>().ReverseMap();
        CreateMap<Theme, CreateThemeDto>().ReverseMap();
    }
}