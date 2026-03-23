using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly AppDbContext _dbContext;

    public SubmissionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Submission?> GetByIdAsync(int id)
    {
        return await _dbContext.Submissions
            .FirstOrDefaultAsync(submission => submission.Id == id);
    }

    public async Task<Submission?> GetByParticipantIdAsync(int participantId)
    {
        return await _dbContext.Submissions
            .FirstOrDefaultAsync(submission => submission.ParticipantId == participantId);
    }

    public async Task<bool> ExistsByParticipantIdAsync(int participantId)
    {
        return await _dbContext.Submissions
            .AnyAsync(submission => submission.ParticipantId == participantId);
    }

    public async Task<Submission> AddAsync(Submission submission)
    {
        await _dbContext.Submissions.AddAsync(submission);
        await _dbContext.SaveChangesAsync();
        return submission;
    }

    public async Task UpdateAsync(Submission submission)
    {
        _dbContext.Submissions.Update(submission);
        await _dbContext.SaveChangesAsync();
    }
}
