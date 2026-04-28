using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/submission-settings")]
public class SubmissionSettingsController : ControllerBase
{
    private readonly ISubmissionSettingsService _submissionSettingsService;

    public SubmissionSettingsController(
        ISubmissionSettingsService submissionSettingsService
    )
    {
        _submissionSettingsService = submissionSettingsService;
    }

    [HttpGet("active")]
    public async Task<ActionResult<SubmissionSettingsDto>> GetActive()
    {
        var result = await _submissionSettingsService.GetActiveAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("conference/{conferenceId:int}")]
    public async Task<ActionResult<SubmissionSettingsDto>> GetByConference(
        int conferenceId
    )
    {
        var result =
            await _submissionSettingsService.GetByConferenceIdAsync(conferenceId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("conference/{conferenceId:int}")]
    public async Task<ActionResult<SubmissionSettingsDto>> CreateOrUpdate(
        int conferenceId,
        SubmissionSettingsUpdateDto dto
    )
    {
        var result =
            await _submissionSettingsService.CreateOrUpdateAsync(
                conferenceId,
                dto
            );

        return Ok(result);
    }

    [HttpDelete("conference/{conferenceId:int}")]
    public async Task<IActionResult> Delete(int conferenceId)
    {
        var deleted =
            await _submissionSettingsService.DeleteAsync(conferenceId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}