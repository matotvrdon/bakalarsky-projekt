using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Conference;

namespace Web.Services.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Conference, ConferenceDto>().ReverseMap();
        CreateMap<Conference, CreateConferenceDto>().ReverseMap();
    }
}