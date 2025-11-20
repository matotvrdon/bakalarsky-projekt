using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface ITalkRepository
{
    Task<List<Talk>> GetAllByThemeIdAsync(int themeId);
    Task<Talk?> GetByTalkIdAsync(int talkId);
    Task AddAsync(Talk talk);
}