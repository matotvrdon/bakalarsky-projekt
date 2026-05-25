using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/participant-status-assignments")]
public class ParticipantStatusAssignmentController : ControllerBase
{
    private readonly IParticipantStatusAssignmentService _assignmentService;

    public ParticipantStatusAssignmentController(IParticipantStatusAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [HttpPost("{assignmentId:int}/file")]
    public async Task<IActionResult> UploadFile([FromRoute] int assignmentId, IFormFile file)
    {
        var result = await _assignmentService.UploadFileAsync(assignmentId, file);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("{assignmentId:int}/file")]
    public async Task<IActionResult> DownloadFile(
        [FromRoute] int assignmentId,
        [FromQuery] bool inline = false)
    {
        var result = await _assignmentService.DownloadFileAsync(assignmentId);

        if (result == null)
        {
            return NotFound();
        }

        if (inline)
        {
            Response.Headers.ContentDisposition =
                $"inline; filename=\"{result.Value.FileName}\"";

            return File(
                result.Value.Content,
                result.Value.ContentType
            );
        }

        Response.Headers.ContentDisposition =
            $"attachment; filename=\"{result.Value.FileName}\"";

        return File(
            result.Value.Content,
            result.Value.ContentType,
            result.Value.FileName
        );
    }

    [HttpPost("{assignmentId:int}/approve")]
    public async Task<IActionResult> Approve([FromRoute] int assignmentId)
    {
        var result = await _assignmentService.ApproveAsync(assignmentId);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost("{assignmentId:int}/reject")]
    public async Task<IActionResult> Reject([FromRoute] int assignmentId)
    {
        var result = await _assignmentService.RejectAsync(assignmentId);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}