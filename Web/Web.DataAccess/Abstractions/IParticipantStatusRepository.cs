using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IParticipantStatusRepository
{
    Task<List<ParticipantStatus>> GetByConferenceIdAsync(int conferenceId);

    Task<List<ParticipantStatus>> GetActiveByIdsForConferenceAsync(
        int conferenceId,
        List<int> statusIds
    );

    Task<ParticipantStatus?> GetByIdAsync(int statusId);

    Task<ConferenceSettings?> GetConferenceSettingsByConferenceIdAsync(int conferenceId);

    Task<ParticipantStatus> AddAsync(ParticipantStatus participantStatus);

    Task UpdateAsync(ParticipantStatus participantStatus);

    Task<bool> ExistsByNameAsync(
        int conferenceSettingsId,
        string name,
        int? ignoredStatusId = null
    );
}