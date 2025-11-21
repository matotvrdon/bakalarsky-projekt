using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Talk;

namespace Web.Services.Mapper;

public class TalkProfile : Profile
{
    public TalkProfile()
    {
        CreateMap<Talk, TalkDto>().ReverseMap();
        CreateMap<Talk, CreateTalkDto>().ReverseMap();
        CreateMap<Talk, UpdateTalkDto>().ReverseMap();
    }
}