using AutoMapper;
using Web.Domain.Abstractions;
using Web.Services.Abstractions;
using Web.Services.DTOs.Theme;

namespace Web.Services.Services;

public class ThemeService : IThemeService
{
    private readonly IThemeRepository _themeRepository;
    private readonly IMapper _mapper;

    public ThemeService(IThemeRepository themeRepository, IMapper mapper)
    {
        _themeRepository = themeRepository;
        _mapper = mapper;
    }

    public async Task<List<ThemeDto>> GetAllBySessionIdAsync(int sessionId)
    {
        var themes = await _themeRepository.GetAllBySessionIdAsync(sessionId);
        return _mapper.Map<List<ThemeDto>>(themes);
    }

    public async Task<ThemeDto?> GetByThemeIdAsync(int themeId)
    {
        var theme = await _themeRepository.GetByThemeIdAsync(themeId);
        return _mapper.Map<ThemeDto?>(theme);
    }

    public async Task<ThemeDto> AddAsync(CreateThemeDto createThemeDto)
    {
        var theme = _mapper.Map<Domain.Models.Theme>(createThemeDto);
        await _themeRepository.AddAsync(theme);
        return _mapper.Map<ThemeDto>(theme);
    }
}