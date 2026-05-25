using Microsoft.AspNetCore.Http;
using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IParticipantStatusAssignmentService
{
    Task<StatusAssignmentFileDto?> UploadFileAsync(int assignmentId, IFormFile file);

    Task<(byte[] Content, string ContentType, string FileName)?> DownloadFileAsync(int assignmentId);

    Task<StatusAssignmentFileDto?> ApproveAsync(int assignmentId);

    Task<StatusAssignmentFileDto?> RejectAsync(int assignmentId);
}