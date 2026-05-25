using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IConferenceRepository
{
    Task<Conference?> GetByIdAsync(int id);
    Task<Conference?> GetPublicByIdAsync(int id);
    Task<List<Conference>> GetAllAsync();
    Task<List<Conference>> GetActiveAsync();
    Task<Dictionary<int, int>> GetParticipantCountsAsync(List<int> conferenceIds);
    Task<Conference> AddAsync(Conference conference);
    Task UpdateAsync(Conference conference);
    Task DeleteAsync(Conference conference);
    Task<bool> ExistsAsync(int id);
}