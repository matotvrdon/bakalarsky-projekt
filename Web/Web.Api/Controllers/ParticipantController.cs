using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/participant")]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService, IConfiguration configuration, IWebHostEnvironment env)
    {
        _participantService = participantService;
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ParticipantUpdateDto dto)
    {
        var participant = await _participantService.UpdateAsync(dto);
        if (participant == null)
            return NotFound();
        return Ok(participant);
    }
    
    [HttpGet("by-user/{userId:int}")]
    public async Task<IActionResult> GetByUserId([FromRoute] int userId)
    {
        var participants = await _participantService.GetByUserIdAsync(userId);
        if (participants == null)
            return NotFound();
        return Ok(participants);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var participant = await _participantService.GetAllByActiveConferenceAsync();
            return Ok(participant);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
