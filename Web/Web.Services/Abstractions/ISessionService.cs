using Web.Services.DTOs.Session;

namespace Web.Services.Abstractions;

public interface ISessionService
{
    Task<List<SessionDto>> GetAllByDayIdAsync(int dayId);
    Task<SessionDto?> GetBySessionIdAsync(int sessionId);
    Task<SessionDto> AddAsync(CreateSessionDto createSessionDto);
    Task<SessionDto?> UpdateAsync(UpdateSessionDto updateSessionDto);
    Task<bool> DeleteAsync(int sessionId);
}