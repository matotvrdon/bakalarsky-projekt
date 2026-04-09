using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IConferenceSettingsRepository
{
    Task<Conference?> GetConferenceWithSettingsAsync(int conferenceId);
    Task UpdateConferenceSettingsAsync(Conference conference);
}
