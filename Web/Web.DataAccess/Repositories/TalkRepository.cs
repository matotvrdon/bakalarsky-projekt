using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class TalkRepository : ITalkRepository
{
    private readonly ApplicationDbContext _context;

    public TalkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Talk>> GetAllByThemeIdAsync(int themeId)
    {
        return await _context.Talk
            .Where(t => t.ThemeId == themeId)
            .ToListAsync();
    }

    public async Task<Talk?> GetByTalkIdAsync(int talkId)
    {
        return await _context.Talk
            .FirstOrDefaultAsync(x => x.Id == talkId);
    }

    public async Task AddAsync(Talk talk)
    {
        await _context.Talk.AddAsync(talk);
        await _context.SaveChangesAsync();
    }
}