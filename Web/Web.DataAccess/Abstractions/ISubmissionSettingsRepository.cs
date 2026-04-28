using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface ISubmissionSettingsRepository
{
    Task<SubmissionSettings?> GetByConferenceSettingsIdAsync(int conferenceSettingsId);
    Task<SubmissionSettings?> GetByConferenceIdAsync(int conferenceId);
    Task<SubmissionSettings?> GetByActiveConferenceAsync();

    Task<SubmissionSettings> AddAsync(SubmissionSettings submissionSettings);
    Task UpdateAsync(SubmissionSettings submissionSettings);
    Task DeleteAsync(SubmissionSettings submissionSettings);
}