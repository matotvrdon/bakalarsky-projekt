using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class PageContentRepository : IPageContentRepository
{
    private readonly ApplicationDbContext _context;

    public PageContentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PageContent>> GetAllAsync()
    {
        return await _context.PageContent
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PageContent?> GetByIdAsync(int id)
    {
        return await _context.PageContent
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PageContent?> GetByNavBarMenuIdAsync(int navBarMenuId)
    {
        return await _context.PageContent
            .AsNoTracking()
            .Where(x => x.NavBarMenuId == navBarMenuId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(PageContent pageContent)
    {
        await _context.PageContent.AddAsync(pageContent);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PageContent pageContent)
    {
        _context.PageContent.Update(pageContent);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pageContent = await _context.PageContent.FirstOrDefaultAsync(x => x.Id == id);
        if (pageContent == null)
        {
            return false;
        }

        _context.PageContent.Remove(pageContent);
        await _context.SaveChangesAsync();
        return true;
    }
}
