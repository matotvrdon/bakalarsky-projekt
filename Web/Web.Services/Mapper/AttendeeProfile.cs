using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Attendee;

namespace Web.Services.Mapper;

public class AttendeeProfile : Profile
{
    public AttendeeProfile()
    {
        CreateMap<Attendee, AttendeeDto>().ReverseMap();
        CreateMap<Attendee, CreateAttendeeDto>().ReverseMap();
    }
}
