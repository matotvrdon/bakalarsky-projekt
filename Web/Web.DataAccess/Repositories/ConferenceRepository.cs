using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ConferenceRepository : IConferenceRepository
{
    private readonly AppDbContext _dbContext;

    public ConferenceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Conference?> GetByIdAsync(int id)
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
            .AsNoTracking()
            .FirstOrDefaultAsync(conference => conference.Id == id);
    }

    public async Task<List<Conference>> GetAllAsync()
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
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Conference>> GetActiveAsync()
    {
        return await  _dbContext.Conferences
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
            .AsNoTracking()
            .Where(conf => conf.IsActive)
            .ToListAsync();
    }

    public async Task<Conference> AddAsync(Conference conference)
    {
        await _dbContext.Conferences.AddAsync(conference);
        await _dbContext.SaveChangesAsync();
        return conference;
    }

    public async Task UpdateAsync(Conference conference)
    {
        _dbContext.Conferences.Update(conference);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Conference conference)
    {
        var conferenceToDelete = await _dbContext.Conferences
            .Include(item => item.Settings)
            .ThenInclude(settings => settings!.ImportantDates)
            .Include(item => item.Settings)
            .ThenInclude(settings => settings!.ConferenceEntries)
            .Include(item => item.Settings)
            .ThenInclude(settings => settings!.FoodOptions)
            .Include(item => item.Settings)
            .ThenInclude(settings => settings!.BookingOptions)
            .Include(item => item.Settings)
            .ThenInclude(settings => settings!.ProgramDays)
            .FirstOrDefaultAsync(item => item.Id == conference.Id);
        if (conferenceToDelete == null)
            return;

        if (conferenceToDelete.Settings?.ImportantDates is { Count: > 0 })
        {
            _dbContext.ImportantDates.RemoveRange(conferenceToDelete.Settings.ImportantDates);
        }

        if (conferenceToDelete.Settings != null)
        {
            _dbContext.ConferenceSettings.Remove(conferenceToDelete.Settings);
        }

        _dbContext.Conferences.Remove(conferenceToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return  await _dbContext.Conferences
            .AsNoTracking()
            .AnyAsync(conference => conference.Id == id);
    }
}
