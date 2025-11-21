using Web.Services.DTOs.Theme;

namespace Web.Services.Abstractions;

public interface IThemeService
{
    Task<List<ThemeDto>> GetAllBySessionIdAsync(int sessionId);
    Task<ThemeDto?> GetByThemeIdAsync(int themeId);
    Task<ThemeDto> AddAsync(CreateThemeDto createThemeDto);
    Task<ThemeDto?> UpdateAsync(UpdateThemeDto updateThemeDto);
    Task<bool> DeleteAsync(int themeId);
}