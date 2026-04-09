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
            .FirstOrDefaultAsync(conference => conference.Id == conferenceId);
    }

    public async Task UpdateConferenceSettingsAsync(Conference conference)
    {
        await _dbContext.SaveChangesAsync();
    }
}
