using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Supplier;

namespace Web.Services.Mapper;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<Supplier, SupplierDto>().ReverseMap();
    }
}
