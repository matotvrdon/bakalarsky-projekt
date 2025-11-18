using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Conference;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/conference")]
public class ConferenceController : ControllerBase
{
    
    private readonly IConferenceService _repository;

    public ConferenceController(IConferenceService repository)
    {
        _repository = repository;
    }


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("get-by-id/{id:int}", Name = "GetConferenceById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var conference = await _repository.GetByIdAsync(id);
        if (conference == null)
        {
            return NotFound($"Conference with id {id} not found.");
        }
        return Ok(conference);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateConferenceDto createConferenceDto)
    {
        var result = await _repository.AddAsync(createConferenceDto);
        return CreatedAtAction("GetById", new { id = result.Id }, result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var result =  await _repository.DeleteAsync(id);
        if(!result)
        {
            return NotFound($"Conference with id {id} not found.");
        }
        
        return NoContent();
    }
    
}