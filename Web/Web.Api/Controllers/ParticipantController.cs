using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/participant")]
public class ParticipantController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    // private readonly string _storageRoot;

    public ParticipantController(AppDbContext dbContext, IConfiguration configuration, IWebHostEnvironment env)
    {
        _dbContext = dbContext;
        // var configuredRoot = configuration["FileStorage:RootPath"] ?? "storage";
        // _storageRoot = Path.IsPathRooted(configuredRoot)
        //     ? configuredRoot
        //     : Path.Combine(env.ContentRootPath, configuredRoot);
    }
    
    

    // // [HttpPut]
    // // public async Task<IActionResult> Update([FromBody] ParticipantUpdateDto dto)
    // // {
    // //     var participant = await _dbContext.Participants
    // //         .Include(p => p.StudentVerification)
    // //         .FirstOrDefaultAsync(p => p.Id == dto.Id);
    // //     if (participant == null)
    // //         return NotFound();
    // //
    // //     participant.FirstName = dto.FirstName;
    // //     participant.LastName = dto.LastName;
    // //     participant.Phone = dto.Phone;
    // //     participant.Affiliation = dto.Affiliation;
    // //     participant.Country = dto.Country;
    // //     participant.RegistrationType = dto.RegistrationType;
    // //     participant.UserId = dto.UserId;
    // //     participant.ConferenceId = dto.ConferenceId;
    // //
    // //     if (dto.StudentStatus.HasValue)
    // //     {
    // //         participant.StudentVerification ??= new StudentVerification
    // //         {
    // //             Status = dto.StudentStatus.Value
    // //         };
    // //         participant.StudentVerification.Status = dto.StudentStatus.Value;
    // //     }
    // //
    // //     await _dbContext.SaveChangesAsync();
    // //
    // //     var response = new ParticipantDto
    // //     {
    // //         Id = participant.Id,
    // //         FirstName = participant.FirstName,
    // //         LastName = participant.LastName,
    // //         Phone = participant.Phone,
    // //         Affiliation = participant.Affiliation,
    // //         Country = participant.Country,
    // //         RegistrationType = participant.RegistrationType,
    // //         StudentStatus = participant.StudentVerification?.Status,
    // //         StudentVerificationOriginalFileName = participant.StudentVerification?.OriginalFileName,
    // //         StudentVerificationContentType = participant.StudentVerification?.ContentType,
    // //         StudentVerificationUploadedAt = participant.StudentVerification?.UploadedAt,
    // //         UserId = participant.UserId,
    // //         ConferenceId = participant.ConferenceId
    // //     };
    // //
    // //     return Ok(response);
    // // }
    //
    // [HttpGet("by-user/{userId:int}")]
    // public async Task<IActionResult> GetByUserId(int userId)
    // {
    //     var participant = await _dbContext.Participants
    //         .AsNoTracking()
    //         .Include(p => p.StudentVerification)
    //         .FirstOrDefaultAsync(p => p.UserId == userId);
    //     if (participant == null)
    //         return NotFound();
    //
    //     var participant2 = new ParticipantDto
    //     {
    //         Id = participant.Id,
    //         FirstName = participant.FirstName,
    //         LastName = participant.LastName,
    //         Phone = participant.Phone,
    //         Affiliation = participant.Affiliation,
    //         Country = participant.Country,
    //         RegistrationType = participant.RegistrationType,
    //         StudentStatus = participant.StudentVerification?.Status,
    //         StudentVerificationOriginalFileName = participant.StudentVerification?.OriginalFileName,
    //         StudentVerificationContentType = participant.StudentVerification?.ContentType,
    //         StudentVerificationUploadedAt = participant.StudentVerification?.UploadedAt,
    //         UserId = participant.UserId,
    //         ConferenceId = participant.ConferenceId
    //     };
    //     
    //     return Ok(participant2);
    // }
    //
    // [HttpPost("{participantId:int}/student-verification")]
    // [Consumes("multipart/form-data")]
    // [RequestSizeLimit(10 * 1024 * 1024)]
    // public async Task<IActionResult> UploadStudentVerification(
    //     int participantId,
    //     IFormFile file)
    // {
    //     if (file == null || file.Length == 0)
    //         return BadRequest("File is required.");
    //
    //     var participant = await _dbContext.Participants
    //         .Include(p => p.StudentVerification)
    //         .FirstOrDefaultAsync(p => p.Id == participantId);
    //     if (participant == null)
    //         return NotFound();
    //
    //     var status = participant.StudentVerification?.Status;
    //     if (status is null || status == StudentStatus.NotSelected)
    //         return BadRequest("Participant is not marked as student.");
    //
    //     if (status == StudentStatus.Approved)
    //         return BadRequest("Student verification is already approved.");
    //
    //     var safeOriginalName = Path.GetFileName(file.FileName);
    //     var extension = Path.GetExtension(safeOriginalName);
    //     var fileName = $"{Guid.NewGuid():N}{extension}";
    //     var relativePath = Path.Combine("participants", participantId.ToString(), "student", fileName);
    //     var absolutePath = Path.Combine(_storageRoot, relativePath);
    //     Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);
    //
    //     await using (var stream = System.IO.File.Create(absolutePath))
    //     {
    //         await file.CopyToAsync(stream);
    //     }
    //
    //     participant.StudentVerification ??= new StudentVerification
    //     {
    //         Status = StudentStatus.WaitingForApproval
    //     };
    //     participant.StudentVerification.FilePath = relativePath;
    //     participant.StudentVerification.OriginalFileName = safeOriginalName;
    //     participant.StudentVerification.ContentType = file.ContentType;
    //     participant.StudentVerification.UploadedAt = DateTime.UtcNow;
    //     participant.StudentVerification.Status = StudentStatus.WaitingForApproval;
    //
    //     await _dbContext.SaveChangesAsync();
    //
    //     return Ok(new
    //     {
    //         participant.Id,
    //         StudentStatus = participant.StudentVerification.Status,
    //         StudentVerificationOriginalFileName = participant.StudentVerification.OriginalFileName,
    //         StudentVerificationContentType = participant.StudentVerification.ContentType,
    //         StudentVerificationUploadedAt = participant.StudentVerification.UploadedAt
    //     });
    // }
    //
    // [HttpGet("{participantId:int}/student-verification")]
    // public async Task<IActionResult> GetStudentVerification(int participantId)
    // {
    //     var participant = await _dbContext.Participants
    //         .AsNoTracking()
    //         .Include(p => p.StudentVerification)
    //         .FirstOrDefaultAsync(p => p.Id == participantId);
    //     if (participant == null)
    //         return NotFound();
    //
    //     if (participant.StudentVerification == null ||
    //         string.IsNullOrWhiteSpace(participant.StudentVerification.FilePath))
    //         return NotFound();
    //
    //     var absolutePath = Path.Combine(_storageRoot, participant.StudentVerification.FilePath);
    //     if (!System.IO.File.Exists(absolutePath))
    //         return NotFound();
    //
    //     var contentType = participant.StudentVerification.ContentType ?? "application/octet-stream";
    //     var fileName = participant.StudentVerification.OriginalFileName ?? Path.GetFileName(absolutePath);
    //     return PhysicalFile(absolutePath, contentType, fileName);
    // }
    //
    // [HttpPost("{participantId:int}/student-approve")]
    // public async Task<IActionResult> ApproveStudent(int participantId)
    // {
    //     var participant = await _dbContext.Participants
    //         .Include(p => p.StudentVerification)
    //         .FirstOrDefaultAsync(p => p.Id == participantId);
    //     if (participant == null)
    //         return NotFound();
    //
    //     if (participant.StudentVerification == null ||
    //         string.IsNullOrWhiteSpace(participant.StudentVerification.FilePath))
    //         return BadRequest("No verification file uploaded.");
    //
    //     participant.StudentVerification.Status = StudentStatus.Approved;
    //     await _dbContext.SaveChangesAsync();
    //
    //     return Ok(new { participant.Id, StudentStatus = participant.StudentVerification.Status });
    // }
    //
    // [HttpPost("{participantId:int}/student-reject")]
    // public async Task<IActionResult> RejectStudent(int participantId)
    // {
    //     var participant = await _dbContext.Participants
    //         .Include(p => p.StudentVerification)
    //         .FirstOrDefaultAsync(p => p.Id == participantId);
    //     if (participant == null)
    //         return NotFound();
    //
    //     if (participant.StudentVerification == null ||
    //         string.IsNullOrWhiteSpace(participant.StudentVerification.FilePath))
    //         return BadRequest("No verification file uploaded.");
    //
    //     participant.StudentVerification.Status = StudentStatus.Rejected;
    //     await _dbContext.SaveChangesAsync();
    //
    //     return Ok(new { participant.Id, StudentStatus = participant.StudentVerification.Status });
    // }
}
