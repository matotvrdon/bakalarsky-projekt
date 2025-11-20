using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Session>> GetAllByDayIdAsync(int dayId)
    {
        return await _context.Session
            .Include(s => s.Theme)
                .ThenInclude(t => t.Talk)
            .Where(s => s.DayId == dayId)
            .ToListAsync();
    }

    public async Task<Session?> GetBySessionIdAsync(int sessionId)
    {
        return await _context.Session 
            .Include(s => s.Theme)
                .ThenInclude(t => t.Talk)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task AddAsync(Session session)
    {
        await _context.Session.AddAsync(session);
        await _context.SaveChangesAsync();
    }
}