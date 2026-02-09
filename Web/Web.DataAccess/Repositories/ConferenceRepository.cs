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
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Conference?> GetByIdAsync(int id)
    {
        var conference = await _context.Conference
            .AsNoTracking()
            .Include(c => c.Day)
                .ThenInclude(d => d.Session)
                    .ThenInclude(s => s.Theme)
                        .ThenInclude(t => t.Talk)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (conference == null) return null;
        
        OrderConference(conference);
        return conference;
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

    private void OrderConference(Conference conference)
    {

        conference.Day = conference.Day.OrderBy(d => d.Date).ToList();

        foreach (var day in conference.Day)
        {
            day.Session = day.Session.OrderBy(s => s.Title).ToList();
            foreach (var session in day.Session)
            {
                session.Theme = session.Theme.OrderBy(t => t.StartTime).ToList();
                foreach (var theme in session.Theme)
                {
                    theme.Talk = theme.Talk.OrderBy(tk => tk.StartTime).ToList();
                }
            }  
        }
        
    }
}
