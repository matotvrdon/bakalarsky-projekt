using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs;

namespace Web.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        
        CreateMap<Conference, ConferenceDto>()
            .ForMember(dest => dest.ParticipantsCount, opt => opt.Ignore());
        CreateMap<ConferenceCreateDto, Conference>();
        CreateMap<ConferenceUpdateDto, Conference>();

        CreateMap<RegistrationSimpleRequestDto, Participant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<User, Participant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

    }
}
