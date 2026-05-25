using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ParticipantStatusRepository : IParticipantStatusRepository
{
    private readonly AppDbContext _dbContext;

    public ParticipantStatusRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ParticipantStatus>> GetByConferenceIdAsync(int conferenceId)
    {
        return await _dbContext.ParticipantStatuses
            .AsNoTracking()
            .Include(status => status.ConferenceSettings)
            .Where(status => status.ConferenceSettings.ConferenceId == conferenceId)
            .OrderBy(status => status.Order)
            .ThenBy(status => status.Name)
            .ToListAsync();
    }

    public async Task<List<ParticipantStatus>> GetActiveByIdsForConferenceAsync(
        int conferenceId,
        List<int> statusIds)
    {
        if (statusIds.Count == 0)
        {
            return [];
        }

        return await _dbContext.ParticipantStatuses
            .AsNoTracking()
            .Include(status => status.ConferenceSettings)
            .Where(status =>
                statusIds.Contains(status.Id) &&
                status.IsActive &&
                status.ConferenceSettings.ConferenceId == conferenceId
            )
            .ToListAsync();
    }

    public async Task<ParticipantStatus?> GetByIdAsync(int statusId)
    {
        return await _dbContext.ParticipantStatuses
            .Include(status => status.ConferenceSettings)
            .FirstOrDefaultAsync(status => status.Id == statusId);
    }

    public async Task<ConferenceSettings?> GetConferenceSettingsByConferenceIdAsync(int conferenceId)
    {
        return await _dbContext.ConferenceSettings
            .FirstOrDefaultAsync(settings => settings.ConferenceId == conferenceId);
    }

    public async Task<ParticipantStatus> AddAsync(ParticipantStatus participantStatus)
    {
        await _dbContext.ParticipantStatuses.AddAsync(participantStatus);
        await _dbContext.SaveChangesAsync();

        return participantStatus;
    }

    public async Task UpdateAsync(ParticipantStatus participantStatus)
    {
        _dbContext.ParticipantStatuses.Update(participantStatus);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(
        int conferenceSettingsId,
        string name,
        int? ignoredStatusId = null)
    {
        var normalizedName = name.Trim().ToLower();

        return await _dbContext.ParticipantStatuses
            .AsNoTracking()
            .AnyAsync(status =>
                status.ConferenceSettingsId == conferenceSettingsId &&
                status.Name.ToLower() == normalizedName &&
                (!ignoredStatusId.HasValue || status.Id != ignoredStatusId.Value)
            );
    }
}