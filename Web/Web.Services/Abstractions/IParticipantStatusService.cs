using Web.Services.DTOs;
using Web.Services.DTOs.ParticipantStatus;

namespace Web.Services.Abstractions;

public interface IParticipantStatusService
{
    Task<List<ParticipantStatusDto>> GetByConferenceIdAsync(int conferenceId);

    Task<ParticipantStatusDto?> CreateAsync(
        int conferenceId,
        ParticipantStatusCreateDto dto
    );

    Task<ParticipantStatusDto?> UpdateAsync(
        int conferenceId,
        int statusId,
        ParticipantStatusUpdateDto dto
    );

    Task<bool> DeactivateAsync(int conferenceId, int statusId);
}