using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class ParticipantStatusAssignmentRepository : IParticipantStatusAssignmentRepository
{
    private readonly AppDbContext _dbContext;

    public ParticipantStatusAssignmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ParticipantStatusAssignment?> GetByIdAsync(int assignmentId)
    {
        return await _dbContext.ParticipantStatusAssignments
            .Include(assignment => assignment.Participant)
            .Include(assignment => assignment.ParticipantStatus)
            .Include(assignment => assignment.FileManager)
            .FirstOrDefaultAsync(assignment => assignment.Id == assignmentId);
    }

    public async Task<ParticipantStatusAssignment?> GetByIdWithFileAsync(int assignmentId)
    {
        return await _dbContext.ParticipantStatusAssignments
            .Include(assignment => assignment.Participant)
            .Include(assignment => assignment.ParticipantStatus)
            .Include(assignment => assignment.FileManager)
            .FirstOrDefaultAsync(assignment => assignment.Id == assignmentId);
    }

    public async Task UpdateAsync(ParticipantStatusAssignment assignment)
    {
        _dbContext.ParticipantStatusAssignments.Update(assignment);
        await _dbContext.SaveChangesAsync();
    }
}