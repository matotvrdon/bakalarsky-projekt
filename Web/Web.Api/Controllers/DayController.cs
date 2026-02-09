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
        return CreatedAtRoute("GetDayById", new { dayId = result.Id }, result);
    }

    [HttpPut("update-day", Name = "UpdateDay")]
    public async Task<IActionResult> UpdateDayAsync([FromBody] UpdateDayDto updateDayDto)
    {
        var updatedDay = await _dayService.UpdateAsync(updateDayDto);

        if(updatedDay == null)
        {
            return NotFound($"Day with id {updateDayDto.Id} not found.");
        }

        return Ok(updatedDay);
    }

    [HttpDelete("delete-day/{dayId:int}", Name = "DeleteDay")]
    public async Task<IActionResult> DeleteDayAsync([FromRoute] int dayId)
    {
        var isDeleted = await _dayService.DeleteAsync(dayId);

        if(!isDeleted) {
            return NotFound($"Day with id {dayId} not found.");
        }

        return NoContent();
    }
}
