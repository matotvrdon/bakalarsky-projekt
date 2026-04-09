using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/conference/{conferenceId:int}")]
public class ConferenceSettingsController : ControllerBase
{
    private readonly IConferenceSettingsService _conferenceSettingsService;

    public ConferenceSettingsController(IConferenceSettingsService conferenceSettingsService)
    {
        _conferenceSettingsService = conferenceSettingsService;
    }

    [HttpPost("conference-settings")]
    public async Task<IActionResult> Create(int conferenceId, [FromBody] ConferenceSettingsCreateDto dto)
    {
        var settings = await _conferenceSettingsService.CreateAsync(conferenceId, dto);
        if (settings == null)
            return NotFound();

        return Ok(settings);
    }

    [HttpPut("conference-settings/{importantDateId:int}")]
    public async Task<IActionResult> Update(int conferenceId, int importantDateId, [FromBody] ImportantDatesUpdatedDateDto dto)
    {
        var importantDate = await _conferenceSettingsService.UpdateAsync(conferenceId, importantDateId, dto);
        if (importantDate == null)
            return NotFound();

        return Ok(importantDate);
    }
}
