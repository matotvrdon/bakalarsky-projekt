using AutoMapper;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.DTOs;
using Web.Services.DTOs.Committees;

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
        
        CreateMap<ConferenceCommittee, CommitteeDto>();
        CreateMap<CommitteeRole, CommitteeRoleDto>();
        CreateMap<CommitteeMember, CommitteeMemberDto>();

        CreateMap<CommitteeCreateDto, ConferenceCommittee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CommitteeUpdateDto, ConferenceCommittee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CommitteeRoleCreateDto, CommitteeRole>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceCommitteeId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceCommittee, opt => opt.Ignore())
            .ForMember(dest => dest.Members, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CommitteeRoleUpdateDto, CommitteeRole>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceCommitteeId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceCommittee, opt => opt.Ignore())
            .ForMember(dest => dest.Members, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CommitteeMemberCreateDto, CommitteeMember>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CommitteeRoleId, opt => opt.Ignore())
            .ForMember(dest => dest.CommitteeRole, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CommitteeMemberUpdateDto, CommitteeMember>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CommitteeRoleId, opt => opt.Ignore())
            .ForMember(dest => dest.CommitteeRole, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<SubmissionSettings, SubmissionSettingsDto>();

        CreateMap<SubmissionSettingsCreateDto, SubmissionSettings>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore());

        CreateMap<SubmissionSettingsUpdateDto, SubmissionSettings>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettingsId, opt => opt.Ignore())
            .ForMember(dest => dest.ConferenceSettings, opt => opt.Ignore());
        
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(
                dest => dest.IsShared,
                opt => opt.MapFrom(src => src.Type == InvoiceType.Shared)
            );
        CreateMap<InvoiceItem, InvoiceItemDto>();
        CreateMap<InvoiceParticipant, InvoiceParticipantDto>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.Participant.FirstName} {src.Participant.LastName}")
            );
        
        CreateMap<Supplier, SupplierDto>();
    }
}
