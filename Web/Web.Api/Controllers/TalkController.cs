using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Talk;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/talk")]
public class TalkController : ControllerBase
{
    private readonly ITalkService _talkService;

    public TalkController(ITalkService talkService)
    {
        _talkService = talkService;
    }

    [HttpGet("talk-by-theme-id/{themeId}", Name = "GetTalkByThemeId")]
    public async Task<IActionResult> GetTalkByThemeIdAsync([FromRoute] int themeId)
    {
        var talk = await _talkService.GetAllByThemeIdAsync(themeId);
        
        return Ok(talk);
    }

    [HttpGet("get-by-id/{talkId}", Name = "GetTalkById")]
    public async Task<IActionResult> GetTalkByIdAsync([FromRoute] int talkId)
    {
        var talk = await _talkService.GetByTalkIdAsync(talkId);

        if(talk == null) {
            return NotFound($"Talk with id {talkId} not found.");
        }

        return Ok(talk);
    }
    
    [HttpPost("add-talk")]
    public async Task<IActionResult> AddTalkAsync([FromBody] CreateTalkDto createTalkDto)
    {
        var result = await _talkService.AddAsync(createTalkDto);
        return CreatedAtAction("GetTalkById", new { talkId = result.Id }, result);
    }
}