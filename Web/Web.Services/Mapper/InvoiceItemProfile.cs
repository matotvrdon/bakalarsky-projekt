using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.Mapper;

public class InvoiceItemProfile : Profile
{
    public InvoiceItemProfile()
    {
        CreateMap<InvoiceItem, InvoiceItemDto>().ReverseMap();
        CreateMap<InvoiceItem, CreateInvoiceItemDto>().ReverseMap();
    }
}
