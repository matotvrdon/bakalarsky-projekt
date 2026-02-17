using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IConferenceRepository
{
    Task<Conference?> GetByIdAsync(int id);
    Task<List<Conference>> GetAllAsync();
    Task<List<Conference>> GetActiveAsync();
    Task<Conference> AddAsync(Conference conference);
    Task UpdateAsync(Conference conference);
    Task DeleteAsync(Conference conference);
    Task<bool> ExistsAsync(int id);
}