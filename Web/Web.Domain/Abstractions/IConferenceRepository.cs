using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IConferenceRepository
{
    Task<List<Conference>> GetAllAsync();
    Task<Conference?> GetByIdAsync(int id);
    Task AddAsync(Conference conference);
    Task<bool> DeleteAsync(int id);
}