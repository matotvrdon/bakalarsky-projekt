using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class DayRepository : IDayRepository
{
    private readonly ApplicationDbContext _context;
    
    public DayRepository(ApplicationDbContext context)
    {
        _context = context;

    }
    
    public async Task<List<Day>> GetAllByConferenceIdAsync(int conferenceId)
    {
       return await _context.Day
           .AsNoTracking()
           .Include(d => d.Session)
                .ThenInclude(s => s.Theme)
                    .ThenInclude(t => t.Talk)
           .Where(d => d.ConferenceId == conferenceId)
           .ToListAsync();
    }

    public async Task<Day?> GetByDayIdAsync(int dayId)
    {
        return await _context.Day
            .AsNoTracking()
            .Include(d => d.Session)
                .ThenInclude(s => s.Theme)
                    .ThenInclude(t => t.Talk)
            .FirstOrDefaultAsync(x => x.Id == dayId);
    }

    public async Task AddAsync(Day day)
    {
        await _context.Day.AddAsync(day);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Day day)
    {
        _context.Day.Update(day);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int dayId)
    {
        var day = await _context.Day.FirstOrDefaultAsync(x => x.Id == dayId);
        if (day == null)
        {
            return false;
        }

        _context.Day.Remove(day);
        await _context.SaveChangesAsync();
        return true;
    }
}
