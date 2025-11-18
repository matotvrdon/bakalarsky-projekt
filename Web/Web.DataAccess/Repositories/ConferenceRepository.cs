using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ConferenceRepository : IConferenceRepository
{

    private readonly ApplicationDbContext _context;

    public ConferenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Conference>> GetAllAsync()
    {
        return await _context.Conference
            .ToListAsync();
    }

    public async Task<Conference?> GetByIdAsync(int id)
    {
        return await _context.Conference
            .Include(c => c.Day)
                .ThenInclude(d => d.Session)
                    .ThenInclude(s => s.Theme)
                        .ThenInclude(t => t.Talk)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Conference conference)
    {
        await _context.Conference.AddAsync(conference); 
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Conference.FirstOrDefaultAsync(x => x.Id == id);

        if(entity == null)
        {
            return false;
        }
        _context.Conference.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}