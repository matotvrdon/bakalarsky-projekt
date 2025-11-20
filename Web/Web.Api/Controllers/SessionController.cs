using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Session;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/session")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpGet("session-by-day-id/{dayId:int}", Name = "GetAllByDayId")]
    public async Task<IActionResult> GetAllByDayIdAsync([FromRoute] int dayId)
    {
        var sessions = await _sessionService.GetAllByDayIdAsync(dayId);

        return Ok(sessions);
    }

    [HttpGet("get-by-id/{sessionId:int}", Name = "GetSessionById")]
    public async Task<IActionResult> GetSessionByIdAsync([FromRoute] int sessionId)
    {
        var session = await _sessionService.GetBySessionIdAsync(sessionId);

        if(session == null)
        {
            return NotFound($"Session with id {sessionId} not found.");
        }
        
        return Ok(session);
    }

    [HttpPost("add-session")]
    public async Task<IActionResult> AddSessionAsync([FromBody] CreateSessionDto createSessionDto)
    {
        var result = await _sessionService.AddAsync(createSessionDto);
        return CreatedAtAction("GetSessionById", new { sessionId = result.Id }, result);
    }
}