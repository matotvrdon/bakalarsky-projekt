using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Attendee;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/attendee")]
public class AttendeeController : ControllerBase
{
    private readonly IAttendeeService _attendeeService;

    public AttendeeController(IAttendeeService attendeeService)
    {
        _attendeeService = attendeeService;
    }

    [HttpGet("get-by-customer/{customerId:int}", Name = "GetAllAttendeesByCustomerId")]
    public async Task<IActionResult> GetAllByCustomerIdAsync([FromRoute] int customerId)
    {
        var attendees = await _attendeeService.GetAllAttendeesByCustomerIdAsync(customerId);
        return Ok(attendees);
    }

    [HttpGet("get-by-id/{attendeeId:int}", Name = "GetAttendeeById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int attendeeId)
    {
        var attendee = await _attendeeService.GetAttendeeByIdAsync(attendeeId);

        if(attendee == null) {
            return NotFound($"Attendee with id {attendeeId} not found.");
        }
        return Ok(attendee);
    }

    // [HttpPut("add-customer")]
    // public async Task<IActionResult> UpdateAsync([FromBody] UpdateAttendeeDto updateAttendeeDto)
    // {
    //     var result = await _attendeeService.UpdateAttendeeAsync(updateAttendeeDto);
    //     if (result == null)
    //     {
    //         return NotFound($"Attendee with id {updateAttendeeDto.AttendeeId} not found.");
    //     }
    //     return Ok(result);
    // }

    [HttpPost("create-attendee")]
    public async Task<IActionResult> CreateAsync(CreateAttendeeDto createAttendeeDto)
    {
        var result = await _attendeeService.AddAttendeeAsync(createAttendeeDto);
        return CreatedAtRoute("GetAttendeeById", new { attendeeId = result.Id }, result);
    }
}
