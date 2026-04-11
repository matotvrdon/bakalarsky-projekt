using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/conference")]
public class ConferenceController : ControllerBase
{
    private readonly IConferenceService _conferenceService;

    public ConferenceController(IConferenceService conferenceService)
    {
        _conferenceService = conferenceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var conferences = await _conferenceService.GetAllAsync();
        return Ok(conferences);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var conferences = await _conferenceService.GetActiveAsync();
        return Ok(conferences);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var conference = await _conferenceService.GetByIdAsync(id);
        if (conference == null)
            return NotFound();
        return Ok(conference);
    }

    [HttpGet("{id:int}/program/pdf")]
    public async Task<IActionResult> DownloadProgramPdf(int id)
    {
        var programPdf = await _conferenceService.GenerateProgramPdfAsync(id);
        if (programPdf == null)
            return NotFound();

        return File(programPdf.Value.Content, "application/pdf", programPdf.Value.FileName);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ConferenceCreateDto dto)
    {
        var conference = await _conferenceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = conference.Id }, conference);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ConferenceUpdateDto dto)
    {
        var conference = await _conferenceService.UpdateAsync(id, dto);
        if (conference == null)
            return NotFound();
        return Ok(conference);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _conferenceService.DeleteAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}
