using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface ISessionRepository
{
    Task<List<Session>> GetAllByDayIdAsync(int dayId);
    Task<Session?> GetBySessionIdAsync(int sessionId);
    Task AddAsync(Session session);
}