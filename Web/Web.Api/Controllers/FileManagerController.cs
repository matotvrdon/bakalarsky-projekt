using Microsoft.AspNetCore.Mvc;
using Web.Domain.Enums;
using Web.Services.Abstractions;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/file-manager")]
public class FileManagerController : ControllerBase
{
    private readonly IFileManagerService _fileManagerService;
    
    public FileManagerController(IWebHostEnvironment hostingEnvironment, IFileManagerService fileManagerService)
    {
        _fileManagerService = fileManagerService;
    }

    [HttpPost("upload/{participantId:int}/{fileType}")]
    public async Task<IActionResult> Upload(IFormFile file, [FromRoute] int participantId, [FromRoute] FileType fileType)
    {
        try
        {
            var result = await _fileManagerService.UploadAsync(file, participantId, fileType);
            return Ok(result);
        }
        catch (ArgumentNullException)
        {
            return BadRequest("File is empty");
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("download/{fileManagerId:int}")]
    public async Task<IActionResult> Download([FromRoute] int fileManagerId)
    {
        try
        { 
            var fileManager = await _fileManagerService.DownloadAsync(fileManagerId);
            return PhysicalFile(fileManager.FilePath, fileManager.ContentType, fileManager.FileName, enableRangeProcessing: true);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Záznam v DB sa nenašiel");
        }
        catch (FileNotFoundException)
        {
            return NotFound("Súbor sa nenašiel");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("view/{fileManagerId:int}")]
    public async Task<IActionResult> View([FromRoute] int fileManagerId)
    {
        try
        {
            var fileManager = await _fileManagerService.ViewAsync(fileManagerId);
            return PhysicalFile(fileManager.FilePath, fileManager.ContentType, enableRangeProcessing: true);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{fileManagerId:int}/{email}/approve")]
    public async Task<IActionResult> Approve([FromRoute] int fileManagerId, [FromRoute] string email)
    {
        try
        {
            var fileManager = await _fileManagerService.ApproveAsync(fileManagerId, email);
            return Ok(fileManager);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut("{fileManagerId:int}/{email}/reject")]
    public async Task<IActionResult> Reject([FromRoute] int fileManagerId, [FromRoute] string email)
    {
        try
        {
            var fileManager = await _fileManagerService.RejectAsync(fileManagerId, email);
            return Ok(fileManager);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{participantId:int}")]
    public async Task<IActionResult> GetAll([FromRoute] int participantId)
    {
        try
        {
            var fileManager = await _fileManagerService.GetAllAsync(participantId);
            return Ok(fileManager);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
