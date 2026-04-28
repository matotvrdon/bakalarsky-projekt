using Web.Domain.Enums;
using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IFileManagerRepository
{
    Task<List<FileManager>> GetFileManagerByParticipantIdAsync(int participantId);
    Task<List<FileManager>> GetFileManagerByParticipantIdAsync(int participantId, FileType fileType);
    Task UpdateAsync(FileManager fileManager);
    Task<FileManager?> GetFileManagerByIdAsync(int fileManagerId);
    Task<FileManager?> GetLatestFileManagerByParticipantIdAsync(int participantId, FileType fileType);
    Task<FileManager> AddAsync(FileManager fileManager);
    
}