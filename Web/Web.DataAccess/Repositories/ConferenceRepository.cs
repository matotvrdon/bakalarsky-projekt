using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ConferenceRepository : IConferenceRepository
{
    private readonly AppDbContext _dbContext;

    public ConferenceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Conference?> GetByIdAsync(int id)
    {
        return await _dbContext.Conferences
            .AsNoTracking()
            .FirstOrDefaultAsync(conference => conference.Id == id);
    }

    public async Task<List<Conference>> GetAllAsync()
    {
        return await _dbContext.Conferences
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Conference>> GetActiveAsync()
    {
        return await  _dbContext.Conferences
            .AsNoTracking()
            .Where(conf => conf.IsActive)
            .ToListAsync();
    }

    public async Task<Conference> AddAsync(Conference conference)
    {
        await _dbContext.Conferences.AddAsync(conference);
        await _dbContext.SaveChangesAsync();
        return conference;
    }

    public async Task UpdateAsync(Conference conference)
    {
        _dbContext.Conferences.Update(conference);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Conference conference)
    {
        _dbContext.Conferences.Remove(conference);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return  await _dbContext.Conferences
            .AsNoTracking()
            .AnyAsync(conference => conference.Id == id);
    }
}