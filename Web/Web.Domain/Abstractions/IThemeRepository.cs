using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IThemeRepository
{
    Task<List<Theme>> GetAllBySessionIdAsync(int sessionId);
    Task<Theme?> GetByThemeIdAsync(int themeId);
    Task AddAsync(Theme theme);
}