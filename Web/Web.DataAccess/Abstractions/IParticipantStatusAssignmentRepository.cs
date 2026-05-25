using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IParticipantStatusAssignmentRepository
{
    Task<ParticipantStatusAssignment?> GetByIdAsync(int assignmentId);

    Task<ParticipantStatusAssignment?> GetByIdWithFileAsync(int assignmentId);

    Task UpdateAsync(ParticipantStatusAssignment assignment);
}