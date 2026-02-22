using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;
using Web.Services.Options;

namespace Web.Services.Services;

public class FileManagerService : IFileManagerService
{
    private readonly IHostEnvironment _environment;
    private readonly IParticipantRepository _participantRepository;
    private readonly IFileManagerRepository _managerRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;

    public FileManagerService(IHostEnvironment environment, IParticipantRepository participantRepository, IFileManagerRepository managerRepository, IMapper mapper, IAuthRepository authRepository)
    {
        _environment = environment;
        _participantRepository = participantRepository;
        _managerRepository = managerRepository;
        _mapper = mapper;
        _authRepository = authRepository;
    }

    public async Task<FileManagerDto> UploadAsync(IFormFile file, int participantId, FileType fileType)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentNullException(nameof(file));

        var participant = await _participantRepository.GetByIdAsync(participantId);
        if (participant == null)
            throw new KeyNotFoundException(nameof(participant));
        
        var uploadPath = Path.Combine(_environment.ContentRootPath, "Storage", GeneratePath(participant.ConferenceId, participantId, fileType));

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        
        var fileName = GenerateFileName(participant.FirstName, participant.LastName, fileType, file.FileName);
        
        var filePath = Path.Combine(uploadPath, fileName);
        
        await using(var stream = new FileStream(filePath, FileMode.Create))
            await file.CopyToAsync(stream);

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
        var fileManagerDto = await AddAsync(dto);
        return fileManagerDto;
    }

    public async Task<FileManagerDownloadDto> DownloadAsync(int fileManagerId)
    {
        var fileManager = await _managerRepository.GetFileManagerByIdAsync(fileManagerId);
        if (fileManager == null)
            throw new KeyNotFoundException(nameof(fileManager));
        
        var filePath = fileManager.FilePath;
        
        if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException(nameof(filePath));
        
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
            throw new KeyNotFoundException(nameof(fileManager));
        
        var filePath = fileManager.FilePath;
        
        if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException(nameof(filePath));
        
        var provider = new FileExtensionContentTypeProvider();
        var contentType = provider.TryGetContentType(filePath, out var detectedContentType)
            ? detectedContentType
            : "application/octet-stream";

        return new FileManagerViewDto()
        {
            FilePath = filePath,
            ContentType = contentType,
        };
    }

    public async Task<List<FileManagerDto>> GetAllAsync(int participantId)
    {
        var fileManager = await _managerRepository.GetFileManagerByParticipantIdAsync(participantId);
        if (!fileManager.Any())
            throw new KeyNotFoundException(nameof(fileManager));
        return _mapper.Map<List<FileManagerDto>>(fileManager);
    }

    public async Task<FileManagerDto?> ApproveAsync(int fileManagerId, string email)
    {
        var fileManager = await _managerRepository.GetFileManagerByIdAsync(fileManagerId);
        if (fileManager == null)
            throw new FileNotFoundException(nameof(fileManager));
        
        var user = await _authRepository.GetByEmailAsync(email.Trim().ToLower());
        if (user == null)
            throw new KeyNotFoundException(nameof(user));
        if (user.Role != UserRole.Admin)
            throw new UnauthorizedAccessException();

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
            throw new FileNotFoundException(nameof(fileManager));
        
        var user = await _authRepository.GetByEmailAsync(email.Trim().ToLower());
        if (user == null)
            throw new KeyNotFoundException(nameof(user));
        if (user.Role != UserRole.Admin)
            throw new UnauthorizedAccessException();

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
            FileType.StudentVerification => "StudentVerification",
            FileType.Submission => "Submission",
            _ => ""
        };
    }
    
    private string GeneratePath(int conferenceId, int participantId, FileType fileType)
    {
        return $"{conferenceId}/{participantId}/{EnumToString(fileType)}";
    }

    private string GenerateFileName(string firstName, string lastName, FileType fileType, string fileName)
    {
        return
            $"{firstName}_{lastName}_{EnumToString(fileType)}{Path.GetExtension(fileName)}";
    }
}