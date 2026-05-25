using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly AppDbContext _dbContext;

    public ParticipantRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Participant?> GetByIdAsync(int id)
    {
        return await _dbContext.Participants
            .Include(participant => participant.Conference)
            .Include(participant => participant.ConferenceEntry)
            .Include(participant => participant.User)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.ParticipantStatus)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.FileManager)
            .Include(participant => participant.FileManagers.OrderByDescending(fileManager => fileManager.CreatedAt))
            .FirstOrDefaultAsync(participant => participant.Id == id);
    }

    public async Task<Participant?> GetByUserIdAsync(int userId)
    {
        return await _dbContext.Participants
            .Include(participant => participant.Conference)
            .Include(participant => participant.ConferenceEntry)
            .Include(participant => participant.User)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.ParticipantStatus)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.FileManager)
            .Include(participant => participant.FileManagers.OrderByDescending(fileManager => fileManager.CreatedAt))
            .Where(participant =>
                participant.UserId == userId &&
                participant.Conference != null &&
                participant.Conference.IsPublished &&
                participant.Conference.Status == ConferenceStatus.Active
            )
            .OrderByDescending(participant => participant.ConferenceId)
            .FirstOrDefaultAsync();
    }

    public async Task<Participant?> GetByUserIdConferenceIdAsync(int userId, int conferenceId)
    {
        return await _dbContext.Participants
            .Include(participant => participant.Conference)
            .Include(participant => participant.ConferenceEntry)
            .Include(participant => participant.User)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.ParticipantStatus)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.FileManager)
            .FirstOrDefaultAsync(participant =>
                participant.UserId == userId &&
                participant.ConferenceId == conferenceId
            );
    }

    public async Task UpdateAsync(Participant participant)
    {
        _dbContext.Participants.Update(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Participant participant)
    {
        _dbContext.Participants.Remove(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string email, int conferenceId)
    {
        return await _dbContext.Participants
            .Include(participant => participant.User)
            .AnyAsync(participant =>
                participant.User != null &&
                participant.User.Email == email &&
                participant.ConferenceId == conferenceId
            );
    }

    public async Task<Participant> AddAsync(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await _dbContext.SaveChangesAsync();

        return participant;
    }

    public async Task<List<Participant>> GetAllAsync()
    {
        return await _dbContext.Participants
            .Include(participant => participant.Conference)
            .Include(participant => participant.ConferenceEntry)
            .Include(participant => participant.User)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.ParticipantStatus)
            .Include(participant => participant.StatusAssignments)
            .ThenInclude(assignment => assignment.FileManager)
            .Include(participant => participant.FileManagers.OrderByDescending(fileManager => fileManager.CreatedAt))
            .OrderByDescending(participant => participant.ConferenceId)
            .ThenBy(participant => participant.LastName)
            .ThenBy(participant => participant.FirstName)
            .ToListAsync();
    }
}