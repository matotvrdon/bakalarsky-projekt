using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface ISubmissionService
{
    Task<SubmissionDto> CreateAsync(SubmissionCreateDto dto);
    Task<SubmissionDto?> UpdateAsync(int id, SubmissionUpdateDto dto);
    Task<SubmissionDto?> GetByParticipantIdAsync(int participantId);
}
