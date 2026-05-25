using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ParticipantStatusAssignmentService : IParticipantStatusAssignmentService
{
    private readonly IParticipantStatusAssignmentRepository _assignmentRepository;
    private readonly AppDbContext _dbContext;
    private readonly IHostEnvironment _environment;

    public ParticipantStatusAssignmentService(
        IParticipantStatusAssignmentRepository assignmentRepository,
        AppDbContext dbContext,
        IHostEnvironment environment)
    {
        _assignmentRepository = assignmentRepository;
        _dbContext = dbContext;
        _environment = environment;
    }

    public async Task<StatusAssignmentFileDto?> UploadFileAsync(int assignmentId, IFormFile file)
    {
        if (file.Length == 0)
        {
            return null;
        }

        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null)
        {
            return null;
        }

        if (!assignment.ParticipantStatus.RequiresApproval)
        {
            return null;
        }

        var safeOriginalName = Path.GetFileName(file.FileName);
        var storedFileName = CreateStoredFileName(safeOriginalName);

        var relativeDirectory = Path.Combine(
            "conferences",
            assignment.Participant.ConferenceId.ToString(),
            "participants",
            assignment.ParticipantId.ToString(),
            "status-confirmations",
            assignment.Id.ToString()
        );

        var storageRoot = GetStorageRoot();
        var fullDirectoryPath = Path.Combine(storageRoot, relativeDirectory);
        var fullFilePath = Path.Combine(fullDirectoryPath, storedFileName);

        Directory.CreateDirectory(fullDirectoryPath);

        await using (var stream = new FileStream(fullFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileManager = new FileManager
        {
            ParticipantId = assignment.ParticipantId,
            Participant = assignment.Participant,
            FileType = FileType.StatusConfirmation,
            FileStatus = FileStatus.WaitingForApproval,
            FilePath = fullFilePath,
            FileName = storedFileName,
            OriginalFileName = safeOriginalName,
            CreatedAt = DateTime.UtcNow,
            ReviewedAt = null,
            ReviewedByUserId = null
        };

        await _dbContext.FileManagers.AddAsync(fileManager);
        await _dbContext.SaveChangesAsync();

        assignment.FileManagerId = fileManager.Id;
        assignment.FileManager = fileManager;
        assignment.ApprovalState = StatusApprovalState.WaitingForApproval;

        await _assignmentRepository.UpdateAsync(assignment);

        return MapToDto(assignment);
    }

    public async Task<(byte[] Content, string ContentType, string FileName)?> DownloadFileAsync(int assignmentId)
    {
        var assignment = await _assignmentRepository.GetByIdWithFileAsync(assignmentId);

        if (assignment?.FileManager == null)
        {
            return null;
        }

        if (!File.Exists(assignment.FileManager.FilePath))
        {
            return null;
        }

        var content = await File.ReadAllBytesAsync(assignment.FileManager.FilePath);
        var contentType = GetContentType(assignment.FileManager.OriginalFileName);

        return (
            content,
            contentType,
            assignment.FileManager.OriginalFileName
        );
    }

    public async Task<StatusAssignmentFileDto?> ApproveAsync(int assignmentId)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null)
        {
            return null;
        }

        if (!assignment.ParticipantStatus.RequiresApproval)
        {
            return null;
        }

        if (assignment.FileManagerId == null)
        {
            return null;
        }

        assignment.ApprovalState = StatusApprovalState.Approved;

        if (assignment.FileManager != null)
        {
            assignment.FileManager.FileStatus = FileStatus.Approved;
            assignment.FileManager.ReviewedAt = DateTime.UtcNow;
        }

        await _assignmentRepository.UpdateAsync(assignment);

        return MapToDto(assignment);
    }

    public async Task<StatusAssignmentFileDto?> RejectAsync(int assignmentId)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null)
        {
            return null;
        }

        if (!assignment.ParticipantStatus.RequiresApproval)
        {
            return null;
        }

        if (assignment.FileManagerId == null)
        {
            return null;
        }

        assignment.ApprovalState = StatusApprovalState.Rejected;

        if (assignment.FileManager != null)
        {
            assignment.FileManager.FileStatus = FileStatus.Rejected;
            assignment.FileManager.ReviewedAt = DateTime.UtcNow;
        }

        await _assignmentRepository.UpdateAsync(assignment);

        return MapToDto(assignment);
    }

    private string GetStorageRoot()
    {
        return Path.Combine(_environment.ContentRootPath, "storage");
    }

    private static string CreateStoredFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);

        return $"{Guid.NewGuid():N}{extension}";
    }

    private static StatusAssignmentFileDto MapToDto(ParticipantStatusAssignment assignment)
    {
        return new StatusAssignmentFileDto
        {
            AssignmentId = assignment.Id,
            ParticipantId = assignment.ParticipantId,
            ParticipantStatusId = assignment.ParticipantStatusId,
            StatusName = assignment.ParticipantStatus.Name,
            ApprovalState = assignment.ApprovalState,
            FileManagerId = assignment.FileManagerId,
            OriginalFileName = assignment.FileManager?.OriginalFileName,
            FileName = assignment.FileManager?.FileName,
            FilePath = assignment.FileManager?.FilePath
        };
    }

    private static string GetContentType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();

        return provider.TryGetContentType(fileName, out var contentType)
            ? contentType
            : "application/octet-stream";
    }
}