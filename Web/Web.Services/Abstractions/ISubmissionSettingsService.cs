using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface ISubmissionSettingsService
{
    Task<SubmissionSettingsDto?> GetByConferenceIdAsync(int conferenceId);
    Task<SubmissionSettingsDto?> GetActiveAsync();

    Task<SubmissionSettingsDto> CreateOrUpdateAsync(
        int conferenceId,
        SubmissionSettingsUpdateDto dto
    );

    Task<bool> DeleteAsync(int conferenceId);
}