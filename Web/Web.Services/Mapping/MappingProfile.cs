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
        CreateMap<ConferenceSettingsCreateDto, ConferenceSettings>();
        CreateMap<ConferenceSettingsUpdateDto, ConferenceSettings>();
        CreateMap<ConferenceSettings, ConferenceSettingsDto>();
        CreateMap<ConferenceEntryCreateDto, ConferenceEntry>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore());
        CreateMap<ConferenceEntry, ConferenceEntryDto>();
        CreateMap<ImportantDatesUpdateDto, ImportantDates>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore());
        CreateMap<ImportantDates, ImportantDatesDto>();
        CreateMap<FoodOptionsCreateDto, FoodOptions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore());
        CreateMap<FoodOptions, FoodOptionsDto>();
        CreateMap<BookingOptionsCreateDto, BookingOptions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore());
        CreateMap<BookingOptions, BookingOptionsDto>();
        CreateMap<ProgramDay, ProgramDayDto>();
        CreateMap<ProgramItem, ProgramItemDto>()
            .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.ProgramSessions));
        CreateMap<ProgramSession, ProgramSessionDto>()
            .ForMember(dest => dest.Presentations, opt => opt.MapFrom(src => src.ProgramPresentations));
        CreateMap<ProgramPresentation, ProgramPresentationDto>();

        CreateMap<RegistrationSimpleRequestDto, Participant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<RegistrationBasicRequestDto, Participant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        CreateMap<User, Participant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Participant, ParticipantDto>();
        CreateMap<ParticipantUpdateDto, Participant>()
            .ForMember(dest => dest.ConferenceEntry, opt => opt.Ignore());

        CreateMap<FileManagerCreateDto, FileManager>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Participant, opt => opt.Ignore());
        CreateMap<FileManager, FileManagerDto>();

        CreateMap<SubmissionCreateDto, Submission>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Participant, opt => opt.Ignore())
            .ForMember(dest => dest.Conference, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<SubmissionUpdateDto, Submission>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Participant, opt => opt.Ignore())
            .ForMember(dest => dest.Conference, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<Submission, SubmissionDto>();
    }
}
