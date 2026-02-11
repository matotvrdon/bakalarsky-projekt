using Web.Services.DTOs.PageContent;

namespace Web.Services.Abstractions;

public interface IPageContentService
{
    Task<List<PageContentDto>> GetAllAsync();
    Task<PageContentDto?> GetByIdAsync(int id);
    Task<PageContentDto?> GetByNavBarMenuIdAsync(int navBarMenuId);
    Task<PageContentDto> AddAsync(CreatePageContentDto createPageContentDto);
    Task<PageContentDto?> UpdateAsync(UpdatePageContentDto updatePageContentDto);
    Task<bool> DeleteAsync(int id);
}
