using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface ISubmissionRepository
{
    Task<Submission?> GetByIdAsync(int id);
    Task<Submission?> GetByParticipantIdAsync(int participantId);
    Task<bool> ExistsByParticipantIdAsync(int participantId);
    Task<Submission> AddAsync(Submission submission);
    Task UpdateAsync(Submission submission);
}
