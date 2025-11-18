using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Day;

namespace Web.Services.Mapper;

public class DayProfile : Profile
{
    public DayProfile()
    {
        CreateMap<Day, DayDto>().ReverseMap();
    }
}