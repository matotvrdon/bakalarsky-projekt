using Microsoft.AspNetCore.Http;
using Web.Domain.Enums;
using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IFileManagerService
{
    Task<FileManagerDto> UploadAsync(IFormFile file, int participantId, FileType fileType);

    Task<FileManagerDto> StoreGeneratedFileAsync(
        byte[] content,
        int participantId,
        FileType fileType,
        string originalFileName,
        string? fileName = null);

    Task<FileManagerDownloadDto> DownloadAsync(int fileManagerId);

    Task<FileManagerViewDto> ViewAsync(int fileManagerId);

    Task<List<FileManagerDto>> GetAllAsync(int participantId);

    Task<FileManagerDto?> ApproveAsync(int fileManagerId, string email);

    Task<FileManagerDto?> RejectAsync(int fileManagerId, string email);
}