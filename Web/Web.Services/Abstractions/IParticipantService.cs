using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IParticipantService
{
    Task<ParticipantDto?> GetByUserIdAsync(int userId);

    Task<List<ParticipantDto>> GetAllAsync();

    Task<ParticipantDto?> UpdateAsync(ParticipantDto dto);

    Task<ParticipantDto?> UpdateStatusAssignmentsAsync(
        int participantId,
        ParticipantStatusAssignmentsUpdateDto dto
    );
}