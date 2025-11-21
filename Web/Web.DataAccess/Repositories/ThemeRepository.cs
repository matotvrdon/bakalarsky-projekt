using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ThemeRepository : IThemeRepository
{
    private readonly ApplicationDbContext _context;

    public ThemeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Theme>> GetAllBySessionIdAsync(int sessionId)
    {
        return await _context.Theme
            .Include(t => t.Talk)
            .Where(t => t.SessionId == sessionId)
            .ToListAsync();
    }

    public async Task<Theme?> GetByThemeIdAsync(int themeId)
    {
        return await _context.Theme
            .Include(t => t.Talk)
            .FirstOrDefaultAsync(x => x.Id == themeId);
    }

    public async Task AddAsync(Theme theme)
    {
        await _context.Theme.AddAsync(theme);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Theme theme)
    {
        _context.Theme.Update(theme);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int themeId)
    {
        var theme = await _context.Theme.FirstOrDefaultAsync(x => x.Id == themeId);
        if (theme == null)
        {
            return false;
        }

        _context.Theme.Remove(theme);
        await _context.SaveChangesAsync();
        return true;
    }
}