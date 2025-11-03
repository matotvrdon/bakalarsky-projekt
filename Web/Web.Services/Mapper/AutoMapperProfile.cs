using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Conference;
using Web.Services.DTOs.Invoice;

namespace Web.Services.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Conference, ConferenceDto>().ReverseMap();
        CreateMap<Conference, CreateConferenceDto>().ReverseMap();
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<Invoice, CreateInvoiceDto>();
    }
}