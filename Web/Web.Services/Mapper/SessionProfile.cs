using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Session;

namespace Web.Services.Mapper;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<Session, SessionDto>().ReverseMap();
    }
}