using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Conference;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/conference")]
public class ConferenceController : ControllerBase
{
    
    private readonly IConferenceService _confererenceService;
    private readonly IEventPublisherService _eventPublisherService;

    public ConferenceController(IConferenceService conferenceService, IEventPublisherService eventPublisherService)
    {
        _confererenceService = conferenceService;
        _eventPublisherService = eventPublisherService;
    }


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _confererenceService.GetAllAsync());
    }

    [HttpGet("get-by-id/{id:int}", Name = "GetConferenceById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var conference = await _confererenceService.GetByIdAsync(id);
        if (conference == null)
        {
            return NotFound($"Conference with id {id} not found.");
        }
        return Ok(conference);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateConferenceDto createConferenceDto)
    {
        var result = await _confererenceService.AddAsync(createConferenceDto);
        
        _eventPublisherService.PublishConferenceAdded(result);
        
        return CreatedAtAction("GetById", new { id = result.Id }, result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var result =  await _confererenceService.DeleteAsync(id);
        if(!result)
        {
            return NotFound($"Conference with id {id} not found.");
        }
        
        return NoContent();
    }
    
    
    [HttpGet("subscribe-to-events")]
    public IResult StreamUpdates(CancellationToken cancellationToken)
    {
        var eventStream = _eventPublisherService.SubscribeToConferenceEvents(cancellationToken);
        
        return TypedResults.ServerSentEvents(eventStream);
    }
}