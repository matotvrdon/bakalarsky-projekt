using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class CommitteeRepository : ICommitteeRepository
{
    private readonly AppDbContext _context;

    public CommitteeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConferenceCommittee>> GetByConferenceIdAsync(int conferenceId)
    {
        return await _context.ConferenceCommittees
            .AsNoTracking()
            .Where(committee => committee.ConferenceSettings.ConferenceId == conferenceId)
            .Include(committee => committee.ConferenceSettings)
            .ThenInclude(settings => settings.Conference)
            .Include(committee => committee.Roles)
            .ThenInclude(role => role.Members)
            .OrderBy(committee => committee.Order)
            .ToListAsync();
    }

    public async Task<List<ConferenceCommittee>> GetByActiveConferenceAsync()
    {
        return await _context.ConferenceCommittees
            .AsNoTracking()
            .Where(committee =>
                committee.ConferenceSettings.Conference.IsPublished &&
                committee.ConferenceSettings.Conference.Status == ConferenceStatus.Active
            )
            .Include(committee => committee.ConferenceSettings)
            .ThenInclude(settings => settings.Conference)
            .Include(committee => committee.Roles)
            .ThenInclude(role => role.Members)
            .OrderBy(committee => committee.Order)
            .ToListAsync();
    }

    public async Task<ConferenceSettings?> GetSettingsByConferenceIdAsync(int conferenceId)
    {
        return await _context.ConferenceSettings
            .FirstOrDefaultAsync(settings => settings.ConferenceId == conferenceId);
    }

    public async Task<ConferenceCommittee?> GetCommitteeByIdAsync(int committeeId)
    {
        return await _context.ConferenceCommittees
            .Include(committee => committee.ConferenceSettings)
            .ThenInclude(settings => settings.Conference)
            .Include(committee => committee.Roles)
            .ThenInclude(role => role.Members)
            .FirstOrDefaultAsync(committee => committee.Id == committeeId);
    }

    public async Task<CommitteeRole?> GetRoleByIdAsync(int roleId)
    {
        return await _context.CommitteeRoles
            .Include(role => role.ConferenceCommittee)
            .ThenInclude(committee => committee.ConferenceSettings)
            .ThenInclude(settings => settings.Conference)
            .Include(role => role.Members)
            .FirstOrDefaultAsync(role => role.Id == roleId);
    }

    public async Task<CommitteeMember?> GetMemberByIdAsync(int memberId)
    {
        return await _context.CommitteeMembers
            .Include(member => member.CommitteeRole)
            .ThenInclude(role => role.ConferenceCommittee)
            .ThenInclude(committee => committee.ConferenceSettings)
            .ThenInclude(settings => settings.Conference)
            .FirstOrDefaultAsync(member => member.Id == memberId);
    }

    public async Task<ConferenceCommittee> AddCommitteeAsync(ConferenceCommittee committee)
    {
        await _context.ConferenceCommittees.AddAsync(committee);
        await _context.SaveChangesAsync();

        return committee;
    }

    public async Task<CommitteeRole> AddRoleAsync(CommitteeRole role)
    {
        await _context.CommitteeRoles.AddAsync(role);
        await _context.SaveChangesAsync();

        return role;
    }

    public async Task<CommitteeMember> AddMemberAsync(CommitteeMember member)
    {
        await _context.CommitteeMembers.AddAsync(member);
        await _context.SaveChangesAsync();

        return member;
    }

    public async Task UpdateCommitteeAsync(ConferenceCommittee committee)
    {
        _context.ConferenceCommittees.Update(committee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRoleAsync(CommitteeRole role)
    {
        _context.CommitteeRoles.Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMemberAsync(CommitteeMember member)
    {
        _context.CommitteeMembers.Update(member);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommitteeAsync(ConferenceCommittee committee)
    {
        _context.ConferenceCommittees.Remove(committee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(CommitteeRole role)
    {
        _context.CommitteeRoles.Remove(role);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMemberAsync(CommitteeMember member)
    {
        _context.CommitteeMembers.Remove(member);
        await _context.SaveChangesAsync();
    }
}