using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Conference;
using Web.Services.DTOs.Customer;
using Web.Services.DTOs.Invoice;
using Web.Services.DTOs.InvoiceItem;
using Web.Services.DTOs.Supplier;

namespace Web.Services.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Conference, ConferenceDto>().ReverseMap();
        CreateMap<Conference, CreateConferenceDto>().ReverseMap();
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
        CreateMap<Invoice, CreateInvoiceDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<CreateInvoiceDto, InvoiceDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<InvoiceItem, InvoiceItemDto>().ReverseMap();
        CreateMap<CreateInvoiceItemDto, InvoiceItem>().ReverseMap();
    }
}