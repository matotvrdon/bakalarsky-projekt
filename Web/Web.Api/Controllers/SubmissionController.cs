using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;
using Web.Services.Exceptions;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/submission")]
public class SubmissionController : ControllerBase
{
    private readonly ISubmissionService _submissionService;

    public SubmissionController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SubmissionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SubmissionErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(SubmissionErrorResponseDto), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] SubmissionCreateDto dto)
    {
        try
        {
            var result = await _submissionService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (SubmissionFlowException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Response);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SubmissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SubmissionErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(SubmissionErrorResponseDto), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SubmissionUpdateDto dto)
    {
        try
        {
            var result = await _submissionService.UpdateAsync(id, dto);
            if (result == null)
            {
                return NotFound(new SubmissionErrorResponseDto
                {
                    Code = "SUBMISSION_NOT_FOUND",
                    Message = $"Submission with id {id} was not found.",
                    Field = "id"
                });
            }

            return Ok(result);
        }
        catch (SubmissionFlowException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Response);
        }
    }

    [HttpGet("by-participant/{participantId:int}")]
    [ProducesResponseType(typeof(SubmissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SubmissionErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByParticipantId([FromRoute] int participantId)
    {
        var result = await _submissionService.GetByParticipantIdAsync(participantId);
        if (result == null)
        {
            return NotFound(new SubmissionErrorResponseDto
            {
                Code = "SUBMISSION_NOT_FOUND",
                Message = $"Submission for participant {participantId} was not found.",
                Field = "participantId"
            });
        }

        return Ok(result);
    }
}
