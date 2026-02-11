using AutoMapper;
using Web.Domain.Models;
using Web.Services.DTOs.PageContent;

namespace Web.Services.Mapper;

public class PageContentProfile : Profile
{
    public PageContentProfile()
    {
        CreateMap<PageContent, PageContentDto>().ReverseMap();
        CreateMap<PageContent, CreatePageContentDto>().ReverseMap();
        CreateMap<UpdatePageContentDto, PageContent>();
    }
}
