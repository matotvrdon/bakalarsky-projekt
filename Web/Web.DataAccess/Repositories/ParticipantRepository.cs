using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
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
        return  await _dbContext.Participants.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsAsync(string email, int conferenceId)
    {
        return await _dbContext.Participants
            .Include(p=> p.User)
            .AnyAsync(p => p.User.Email == email && p.ConferenceId == conferenceId);
    }

    public async Task<Participant> AddAsync(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await  _dbContext.SaveChangesAsync();
        return participant;
    }
}