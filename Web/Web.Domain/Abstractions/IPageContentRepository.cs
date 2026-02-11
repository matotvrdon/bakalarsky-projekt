using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IPageContentRepository
{
    Task<List<PageContent>> GetAllAsync();
    Task<PageContent?> GetByIdAsync(int id);
    Task<PageContent?> GetByNavBarMenuIdAsync(int navBarMenuId);
    Task AddAsync(PageContent pageContent);
    Task UpdateAsync(PageContent pageContent);
    Task<bool> DeleteAsync(int id);
}
