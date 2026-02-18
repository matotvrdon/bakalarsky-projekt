using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IParticipantRepository
{
    Task<Participant?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(string email, int conferenceId);
    Task<Participant> AddAsync(Participant participant);
}