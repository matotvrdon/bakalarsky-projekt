using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IParticipantRepository
{
    Task<Participant?> GetByIdAsync(int id);

    Task<Participant?> GetByUserIdAsync(int userId);

    Task<Participant?> GetByUserIdConferenceIdAsync(int userId, int conferenceId);

    Task UpdateAsync(Participant participant);

    Task DeleteAsync(Participant participant);

    Task<bool> ExistsAsync(string email, int conferenceId);

    Task<Participant> AddAsync(Participant participant);

    Task<List<Participant>> GetAllAsync();
}