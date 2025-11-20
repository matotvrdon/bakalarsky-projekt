using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Day;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/day")]
public class DayController : ControllerBase
{
    private readonly IDayService _dayService;

    public DayController(IDayService dayService)
    {
        _dayService = dayService;
    }
    
    [HttpGet("day-by-conference-id/{conferenceId:int}", Name = "GetAllByConferenceId")]
    public async Task<IActionResult> GetAllByConferenceIdAsync([FromRoute] int conferenceId)
    {
        var days = await _dayService.GetAllByConferenceIdAsync(conferenceId);

        return Ok(days);
    }

    [HttpGet("get-by-id/{dayId:int}", Name = "GetDayById")]
    public async Task<IActionResult> GetDayByIdAsync([FromRoute] int dayId)
    {
        var day = await _dayService.GetByDayIdAsync(dayId);

        if(day == null)
        {
            return NotFound($"Day with id {dayId} not found.");
        }
        
        return Ok(day);
    }
    
    [HttpPost("add-day")]
    public async Task<IActionResult> AddDayAsync([FromBody] CreateDayDto createDayDto)
    {
        var result = await _dayService.AddAsync(createDayDto);
        return CreatedAtAction("GetDayById", new { dayId = result.Id }, result);
    }
}