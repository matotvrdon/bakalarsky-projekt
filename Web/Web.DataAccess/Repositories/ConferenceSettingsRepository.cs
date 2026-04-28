using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ConferenceSettingsRepository : IConferenceSettingsRepository
{
    private readonly AppDbContext _dbContext;

    public ConferenceSettingsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Conference?> GetConferenceWithSettingsAsync(int conferenceId)
    {
        return await _dbContext.Conferences
            .Include(conference => conference.Settings)
            .ThenInclude(settings => settings!.ImportantDates)
            .Include(conference => conference.Settings)
            .ThenInclude(settings => settings!.ConferenceEntries)
            .Include(conference => conference.Settings)
            .ThenInclude(settings => settings!.FoodOptions)
            .Include(conference => conference.Settings)
            .ThenInclude(settings => settings!.BookingOptions)
            .Include(conference => conference.Settings)
            .ThenInclude(settings => settings!.ProgramDays)
            .ThenInclude(day => day.ProgramItems)
            .ThenInclude(item => item.ProgramSessions)
            .ThenInclude(session => session.ProgramPresentations)
            .FirstOrDefaultAsync(conference => conference.Id == conferenceId);
    }

    public async Task UpdateConferenceSettingsAsync(Conference conference)
    {
        await _dbContext.SaveChangesAsync();
    }
}
