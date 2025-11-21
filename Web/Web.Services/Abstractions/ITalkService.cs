using Web.Services.DTOs.Talk;

namespace Web.Services.Abstractions;

public interface ITalkService
{
    Task<List<TalkDto>> GetAllByThemeIdAsync(int themeId);
    Task<TalkDto?> GetByTalkIdAsync(int talkId);
    Task<TalkDto> AddAsync(CreateTalkDto createTalkDto);
    Task<TalkDto?> UpdateAsync(UpdateTalkDto updateTalkDto);
    Task<bool> DeleteAsync(int talkId);
}