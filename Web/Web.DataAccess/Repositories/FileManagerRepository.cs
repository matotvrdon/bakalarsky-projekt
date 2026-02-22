using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class FileManagerRepository : IFileManagerRepository
{
    private readonly AppDbContext _dbContext;

    public FileManagerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<FileManager>> GetFileManagerByParticipantIdAsync(int participantId)
    {
        var fileManagers = await _dbContext.FileManagers
            .Where(x => x.ParticipantId == participantId)
            .ToListAsync();
        return fileManagers;
    }

    public async Task<List<FileManager>> GetFileManagerByParticipantIdAsync(int participantId, FileType fileType)
    {
        var fileManagers = await _dbContext.FileManagers
            .Where(x => x.ParticipantId == participantId && x.FileType == fileType)
            .ToListAsync();
        return fileManagers;
    }

    public async Task UpdateAsync(FileManager fileManager)
    {
        _dbContext.FileManagers.Update(fileManager);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FileManager?> GetFileManagerByIdAsync(int fileManagerId)
    {
        return await _dbContext.FileManagers
            .FirstOrDefaultAsync(x => x.Id == fileManagerId);
    }

    public async Task<FileManager?> GetLatestFileManagerByParticipantIdAsync(int participantId, FileType fileType)
    {
        var fileManager = await _dbContext.FileManagers
            .Where(x => x.ParticipantId == participantId && x.FileType == fileType)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
        return fileManager;
    }

    public async Task<FileManager> AddAsync(FileManager fileManager)
    {
        await _dbContext.FileManagers.AddAsync(fileManager);
        await _dbContext.SaveChangesAsync();
        return fileManager;
    }
}