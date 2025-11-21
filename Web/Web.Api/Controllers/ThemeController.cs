using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Theme;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/theme")]
public class ThemeController : ControllerBase
{
    private readonly IThemeService _themeService;

    public ThemeController(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [HttpGet("theme-by-session-id/{sessionId}", Name = "GetThemeBySessionId")]
    public async Task<IActionResult> GetThemeBySessionIdAsync([FromRoute] int sessionId)
    {
        var theme = await _themeService.GetAllBySessionIdAsync(sessionId);

        return Ok(theme);
    }
    
    [HttpGet("get-by-id/{themeId}", Name = "GetThemeById")]
    public async Task<IActionResult> GetThemeByIdAsync([FromRoute] int themeId)
    {
        var theme = await _themeService.GetByThemeIdAsync(themeId);

        if(theme == null)
        {
            return NotFound($"Theme with id {themeId} not found.");
        }
        
        return Ok(theme);
    }
    
    [HttpPost("add-theme")]
    public async Task<IActionResult> AddThemeAsync([FromBody] CreateThemeDto createThemeDto)
    {
        var result = await _themeService.AddAsync(createThemeDto);
        return CreatedAtAction("GetThemeById", new { themeId = result.Id }, result);
    }

    [HttpPut("update-theme", Name = "UpdateTheme")]
    public async Task<IActionResult> UpdateThemeAsync([FromBody] UpdateThemeDto updateThemeDto)
    {
        var updatedTheme = await _themeService.UpdateAsync(updateThemeDto);

        if(updatedTheme == null) 
        {
            return NotFound($"Theme with id {updateThemeDto.Id} not found.");
        }

        return Ok(updatedTheme);
    }
    
    [HttpDelete("delete-theme/{themeId:int}", Name = "DeleteTheme")]
    public async Task<IActionResult> DeleteThemeAsync([FromRoute] int themeId)
    {
        var isDeleted =  await _themeService.DeleteAsync(themeId);

        if(!isDeleted)
        {
            return NotFound($"Theme with id {themeId} not found.");
        }

        return NoContent();
    }
}