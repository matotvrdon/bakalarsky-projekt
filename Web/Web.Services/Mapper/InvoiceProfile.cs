using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Invoice;

namespace Web.Services.Mapper;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
        CreateMap<Invoice, CreateInvoiceDto>().ReverseMap();
        CreateMap<CreateInvoiceDto, InvoiceDto>().ReverseMap();
    }
}
