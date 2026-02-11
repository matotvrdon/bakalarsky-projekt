using AutoMapper;
using Markdig;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.PageContent;

namespace Web.Services.Services;

public class PageContentService : IPageContentService
{
    private readonly IPageContentRepository _pageContentRepository;
    private readonly IMapper _mapper;
    private readonly MarkdownPipeline _markdownPipeline;

    public PageContentService(IPageContentRepository pageContentRepository, IMapper mapper)
    {
        _pageContentRepository = pageContentRepository;
        _mapper = mapper;
        _markdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
    }

    public async Task<List<PageContentDto>> GetAllAsync()
    {
        var pageContents = await _pageContentRepository.GetAllAsync();
        return _mapper.Map<List<PageContentDto>>(pageContents);
    }

    public async Task<PageContentDto?> GetByIdAsync(int id)
    {
        var pageContent = await _pageContentRepository.GetByIdAsync(id);
        return _mapper.Map<PageContentDto?>(pageContent);
    }

    public async Task<PageContentDto?> GetByNavBarMenuIdAsync(int navBarMenuId)
    {
        var pageContent = await _pageContentRepository.GetByNavBarMenuIdAsync(navBarMenuId);
        return _mapper.Map<PageContentDto?>(pageContent);
    }

    public async Task<PageContentDto> AddAsync(CreatePageContentDto createPageContentDto)
    {
        var pageContent = _mapper.Map<PageContent>(createPageContentDto);
        pageContent.Html = Markdown.ToHtml(pageContent.Markdown, _markdownPipeline);
        await _pageContentRepository.AddAsync(pageContent);
        return _mapper.Map<PageContentDto>(pageContent);
    }

    public async Task<PageContentDto?> UpdateAsync(UpdatePageContentDto updatePageContentDto)
    {
        var pageContent = await _pageContentRepository.GetByIdAsync(updatePageContentDto.Id);
        if (pageContent == null)
        {
            return null;
        }

        _mapper.Map(updatePageContentDto, pageContent);
        pageContent.Html = Markdown.ToHtml(pageContent.Markdown, _markdownPipeline);

        await _pageContentRepository.UpdateAsync(pageContent);
        return _mapper.Map<PageContentDto?>(pageContent);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _pageContentRepository.DeleteAsync(id);
    }
}
