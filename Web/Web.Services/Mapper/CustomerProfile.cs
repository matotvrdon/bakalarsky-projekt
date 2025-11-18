using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.Customer;

namespace Web.Services.Mapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
    }
}
