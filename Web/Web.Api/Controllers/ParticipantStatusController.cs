using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/conference/{conferenceId:int}/conference-settings/participant-statuses")]
public class ParticipantStatusController : ControllerBase
{
    private readonly IParticipantStatusService _participantStatusService;

    public ParticipantStatusController(IParticipantStatusService participantStatusService)
    {
        _participantStatusService = participantStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByConferenceId([FromRoute] int conferenceId)
    {
        var statuses = await _participantStatusService.GetByConferenceIdAsync(conferenceId);

        return Ok(statuses);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] int conferenceId,
        [FromBody] ParticipantStatusCreateDto dto
    )
    {
        try
        {
            var status = await _participantStatusService.CreateAsync(conferenceId, dto);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{statusId:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int conferenceId,
        [FromRoute] int statusId,
        [FromBody] ParticipantStatusUpdateDto dto
    )
    {
        try
        {
            var status = await _participantStatusService.UpdateAsync(
                conferenceId,
                statusId,
                dto
            );

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{statusId:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int conferenceId,
        [FromRoute] int statusId
    )
    {
        var result = await _participantStatusService.DeactivateAsync(
            conferenceId,
            statusId
        );

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}