using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/participant")]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    [HttpGet("by-user/{userId:int}")]
    public async Task<ActionResult<ParticipantDto>> GetByUserId([FromRoute] int userId)
    {
        var participant = await _participantService.GetByUserIdAsync(userId);

        if (participant == null)
        {
            return NotFound();
        }

        return Ok(participant);
    }

    [HttpGet]
    public async Task<ActionResult<List<ParticipantDto>>> GetAll()
    {
        var participants = await _participantService.GetAllAsync();

        return Ok(participants);
    }

    [HttpPut]
    public async Task<ActionResult<ParticipantDto>> Update([FromBody] ParticipantDto dto)
    {
        var participant = await _participantService.UpdateAsync(dto);

        if (participant == null)
        {
            return NotFound();
        }

        return Ok(participant);
    }

    [HttpPut("{participantId:int}/status-assignments")]
    public async Task<ActionResult<ParticipantDto>> UpdateStatusAssignments(
        [FromRoute] int participantId,
        [FromBody] ParticipantStatusAssignmentsUpdateDto dto)
    {
        try
        {
            var participant = await _participantService.UpdateStatusAssignmentsAsync(
                participantId,
                dto
            );

            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}