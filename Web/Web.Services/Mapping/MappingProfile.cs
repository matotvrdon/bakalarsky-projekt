using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs;

namespace Web.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Participant, ParticipantDto>();
        CreateMap<ParticipantCreateDto, Participant>();
        CreateMap<ParticipantUpdateDto, Participant>();

        CreateMap<Conference, ConferenceDto>()
            .ForMember(dest => dest.ParticipantsCount, opt => opt.Ignore());
        CreateMap<ConferenceCreateDto, Conference>();
        CreateMap<ConferenceUpdateDto, Conference>();

        CreateMap<ConferenceSettings, ConferenceSettingsDto>()
            .ReverseMap();

        CreateMap<Registration, RegistrationDto>()
            .ReverseMap();

        CreateMap<Submission, SubmissionDto>();
        CreateMap<SubmissionCreateDto, Submission>();
        CreateMap<SubmissionUpdateDto, Submission>();

        CreateMap<SubmissionCategory, SubmissionCategoryDto>()
            .ReverseMap();

        CreateMap<Speaker, SpeakerDto>()
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => SplitList(src.Topics)));
        CreateMap<SpeakerCreateDto, Speaker>()
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => JoinList(src.Topics)));
        CreateMap<SpeakerUpdateDto, Speaker>()
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => JoinList(src.Topics)));

        CreateMap<ScheduleItem, ScheduleItemDto>();
        CreateMap<ScheduleItemCreateDto, ScheduleItem>();
        CreateMap<ScheduleItemUpdateDto, ScheduleItem>();

        CreateMap<InvoiceItem, InvoiceItemDto>()
            .ReverseMap();

        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.ParticipantIds, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.BillingInfo, opt => opt.MapFrom(src => new BillingInfoDto
            {
                CompanyName = src.BillingCompany,
                Ico = src.BillingIco,
                Dic = src.BillingDic,
                Address = src.BillingAddress
            }));

        CreateMap<InvoiceCreateDto, Invoice>()
            .ForMember(dest => dest.BillingCompany, opt => opt.MapFrom(src => src.BillingInfo.CompanyName))
            .ForMember(dest => dest.BillingIco, opt => opt.MapFrom(src => src.BillingInfo.Ico))
            .ForMember(dest => dest.BillingDic, opt => opt.MapFrom(src => src.BillingInfo.Dic))
            .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillingInfo.Address));

        CreateMap<InvoiceUpdateDto, Invoice>()
            .ForMember(dest => dest.BillingCompany, opt => opt.MapFrom(src => src.BillingInfo.CompanyName))
            .ForMember(dest => dest.BillingIco, opt => opt.MapFrom(src => src.BillingInfo.Ico))
            .ForMember(dest => dest.BillingDic, opt => opt.MapFrom(src => src.BillingInfo.Dic))
            .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillingInfo.Address));

        CreateMap<Coupon, CouponDto>()
            .ReverseMap();
        CreateMap<CouponCreateDto, Coupon>();
        CreateMap<CouponUpdateDto, Coupon>();

        CreateMap<AccommodationOption, AccommodationOptionDto>()
            .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => SplitList(src.Amenities)));
        CreateMap<AccommodationOptionDto, AccommodationOption>()
            .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => JoinList(src.Amenities)));

        CreateMap<AccommodationBooking, AccommodationBookingDto>()
            .ReverseMap();

        CreateMap<CateringOption, CateringOptionDto>()
            .ReverseMap();

        CreateMap<CateringOrder, CateringOrderDto>()
            .ForMember(dest => dest.Items, opt => opt.Ignore());
        CreateMap<CateringOrderDto, CateringOrder>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<CateringOrderItem, CateringOrderItemDto>()
            .ReverseMap();
    }

    private static string JoinList(IEnumerable<string> items)
    {
        return string.Join(",", items ?? Array.Empty<string>());
    }

    private static List<string> SplitList(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new List<string>();
        }

        return value.Split(',')
            .Select(item => item.Trim())
            .Where(item => item.Length > 0)
            .ToList();
    }
}
