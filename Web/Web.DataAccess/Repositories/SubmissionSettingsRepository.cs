using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class SubmissionSettingsRepository : ISubmissionSettingsRepository
{
    private readonly AppDbContext _context;

    public SubmissionSettingsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SubmissionSettings?> GetByConferenceSettingsIdAsync(int conferenceSettingsId)
    {
        return await _context.SubmissionSettings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ConferenceSettingsId == conferenceSettingsId);
    }

    public async Task<SubmissionSettings?> GetByConferenceIdAsync(int conferenceId)
    {
        return await _context.SubmissionSettings
            .AsNoTracking()
            .Include(x => x.ConferenceSettings)
            .FirstOrDefaultAsync(x => x.ConferenceSettings.ConferenceId == conferenceId);
    }

    public async Task<SubmissionSettings?> GetByActiveConferenceAsync()
    {
        return await _context.SubmissionSettings
            .AsNoTracking()
            .Include(x => x.ConferenceSettings)
            .ThenInclude(x => x.Conference)
            .FirstOrDefaultAsync(x =>
                x.ConferenceSettings.Conference.IsActive &&
                x.IsEnabled
            );
    }

    public async Task<SubmissionSettings> AddAsync(SubmissionSettings submissionSettings)
    {
        _context.SubmissionSettings.Add(submissionSettings);
        await _context.SaveChangesAsync();
        return submissionSettings;
    }

    public async Task UpdateAsync(SubmissionSettings submissionSettings)
    {
        _context.SubmissionSettings.Update(submissionSettings);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SubmissionSettings submissionSettings)
    {
        _context.SubmissionSettings.Remove(submissionSettings);
        await _context.SaveChangesAsync();
    }
}