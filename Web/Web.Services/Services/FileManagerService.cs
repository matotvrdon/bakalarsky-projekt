using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class FileManagerService : IFileManagerService
{
    private readonly IHostEnvironment _environment;
    private readonly IParticipantRepository _participantRepository;
    private readonly IFileManagerRepository _managerRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;

    public FileManagerService(
        IHostEnvironment environment,
        IParticipantRepository participantRepository,
        IFileManagerRepository managerRepository,
        IMapper mapper,
        IAuthRepository authRepository)
    {
        _environment = environment;
        _participantRepository = participantRepository;
        _managerRepository = managerRepository;
        _mapper = mapper;
        _authRepository = authRepository;
    }

    public async Task<FileManagerDto> UploadAsync(
        IFormFile file,
        int participantId,
        FileType fileType)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentNullException(nameof(file));
        }

        var participant = await _participantRepository.GetByIdAsync(participantId);

        if (participant == null)
        {
            throw new KeyNotFoundException(nameof(participant));
        }

        var uploadPath = Path.Combine(
            _environment.ContentRootPath,
            "Storage",
            GeneratePath(participant.ConferenceId, participantId, fileType)
        );

        Directory.CreateDirectory(uploadPath);

        var fileName = GenerateFileName(
            participant.FirstName,
            participant.LastName,
            fileType,
            file.FileName
        );

        var filePath = Path.Combine(uploadPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var dto = new FileManagerCreateDto
        {
            CreatedAt = DateTime.UtcNow,
            FileName = fileName,
            FilePath = filePath,
            FileStatus = FileStatus.WaitingForApproval,
            FileType = fileType,
            OriginalFileName = file.FileName,
            ParticipantId = participant.Id,
        };

        return await AddAsync(dto);
    }

    public async Task<FileManagerDto> StoreGeneratedFileAsync(
        byte[] content,
        int participantId,
        FileType fileType,
        string originalFileName,
        string? fileName = null)
    {
        if (content.Length == 0)
        {
            throw new ArgumentException("Generated file content is empty.", nameof(content));
        }

        var participant = await _participantRepository.GetByIdAsync(participantId);

        if (participant == null)
        {
            throw new KeyNotFoundException(nameof(participant));
        }

        var uploadPath = Path.Combine(
            _environment.ContentRootPath,
            "Storage",
            GeneratePath(participant.ConferenceId, participantId, fileType)
        );

        Directory.CreateDirectory(uploadPath);

        var storedFileName = string.IsNullOrWhiteSpace(fileName)
            ? GenerateFileName(
                participant.FirstName,
                participant.LastName,
                fileType,
                originalFileName
            )
            : fileName.Trim();

        var filePath = Path.Combine(uploadPath, storedFileName);

        await File.WriteAllBytesAsync(filePath, content);

        var dto = new FileManagerCreateDto
        {
            CreatedAt = DateTime.UtcNow,
            FileName = storedFileName,
            FilePath = filePath,
            FileStatus = FileStatus.Approved,
            FileType = fileType,
            OriginalFileName = originalFileName,
            ParticipantId = participant.Id,
            ReviewedAt = DateTime.UtcNow,
            ReviewedByUserId = null,
        };

        return await AddAsync(dto);
    }

    public async Task<FileManagerDownloadDto> DownloadAsync(int fileManagerId)
    {
        var fileManager = await _managerRepository.GetFileManagerByIdAsync(fileManagerId);

        if (fileManager == null)
        {
            throw new KeyNotFoundException(nameof(fileManager));
        }

        var filePath = fileManager.FilePath;

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(nameof(filePath));
        }

        var provider = new FileExtensionContentTypeProvider();
        var contentType = provider.TryGetContentType(filePath, out var detectedContentType)
            ? detectedContentType
            : "application/octet-stream";

        return new FileManagerDownloadDto
        {
            FilePath = filePath,
            ContentType = contentType,
            FileName = fileManager.OriginalFileName
        };
    }

    public async Task<FileManagerViewDto> ViewAsync(int fileManagerId)
    {
        var fileManager = await _managerRepository.GetFileManagerByIdAsync(fileManagerId);

        if (fileManager == null)
        {
            throw new KeyNotFoundException(nameof(fileManager));
        }

        var filePath = fileManager.FilePath;

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(nameof(filePath));
        }

        var provider = new FileExtensionContentTypeProvider();
        var contentType = provider.TryGetContentType(filePath, out var detectedContentType)
            ? detectedContentType
            : "application/octet-stream";

        return new FileManagerViewDto
        {
            FilePath = filePath,
            ContentType = contentType,
        };
    }

    public async Task<List<FileManagerDto>> GetAllAsync(int participantId)
    {
        var fileManagers = await _managerRepository.GetFileManagerByParticipantIdAsync(participantId);

        if (!fileManagers.Any())
        {
            throw new KeyNotFoundException(nameof(fileManagers));
        }

        return _mapper.Map<List<FileManagerDto>>(fileManagers);
    }

    public async Task<FileManagerDto?> ApproveAsync(int fileManagerId, string email)
    {
        var fileManager = await _managerRepository.GetFileManagerByIdAsync(fileManagerId);

        if (fileManager == null)
        {
            throw new FileNotFoundException(nameof(fileManager));
        }

        var user = await _authRepository.GetByEmailAsync(email.Trim().ToLower());

        if (user == null)
        {
            throw new KeyNotFoundException(nameof(user));
        }

        if (user.Role != UserRole.Admin)
        {
            throw new UnauthorizedAccessException();
        }

        fileManager.FileStatus = FileStatus.Approved;
        fileManager.ReviewedByUserId = user.Id;
        fileManager.ReviewedAt = DateTime.UtcNow;

        await _managerRepository.UpdateAsync(fileManager);

        return _mapper.Map<FileManagerDto>(fileManager);
    }

    public async Task<FileManagerDto?> RejectAsync(int fileManagerId, string email)
    {
        var fileManager = await _managerRepository.GetFileManagerByIdAsync(fileManagerId);

        if (fileManager == null)
        {
            throw new FileNotFoundException(nameof(fileManager));
        }

        var user = await _authRepository.GetByEmailAsync(email.Trim().ToLower());

        if (user == null)
        {
            throw new KeyNotFoundException(nameof(user));
        }

        if (user.Role != UserRole.Admin)
        {
            throw new UnauthorizedAccessException();
        }

        fileManager.FileStatus = FileStatus.Rejected;
        fileManager.ReviewedByUserId = user.Id;
        fileManager.ReviewedAt = DateTime.UtcNow;

        await _managerRepository.UpdateAsync(fileManager);

        return _mapper.Map<FileManagerDto>(fileManager);
    }

    private async Task<FileManagerDto> AddAsync(FileManagerCreateDto dto)
    {
        var fileManager = _mapper.Map<FileManager>(dto);

        await _managerRepository.AddAsync(fileManager);

        return _mapper.Map<FileManagerDto>(fileManager);
    }

    private string EnumToString(FileType fileType)
    {
        return fileType switch
        {
            FileType.StatusConfirmation => "StatusConfirmation",
            FileType.Submission => "Submission",
            FileType.Invoice => "Invoice",
            _ => ""
        };
    }

    private string GeneratePath(int conferenceId, int participantId, FileType fileType)
    {
        return $"{conferenceId}/{participantId}/{EnumToString(fileType)}";
    }

    private string GenerateFileName(
        string firstName,
        string lastName,
        FileType fileType,
        string fileName)
    {
        var safeFirstName = firstName.Trim().Replace(" ", "_");
        var safeLastName = lastName.Trim().Replace(" ", "_");

        return $"{safeFirstName}_{safeLastName}_{EnumToString(fileType)}{Path.GetExtension(fileName)}";
    }
}