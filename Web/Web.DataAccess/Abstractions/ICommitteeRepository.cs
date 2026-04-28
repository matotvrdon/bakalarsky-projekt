using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface ICommitteeRepository
{
    Task<List<ConferenceCommittee>> GetByConferenceIdAsync(int conferenceId);
    Task<List<ConferenceCommittee>> GetByActiveConferenceAsync();

    Task<ConferenceSettings?> GetSettingsByConferenceIdAsync(int conferenceId);

    Task<ConferenceCommittee?> GetCommitteeByIdAsync(int committeeId);
    Task<CommitteeRole?> GetRoleByIdAsync(int roleId);
    Task<CommitteeMember?> GetMemberByIdAsync(int memberId);

    Task<ConferenceCommittee> AddCommitteeAsync(ConferenceCommittee committee);
    Task<CommitteeRole> AddRoleAsync(CommitteeRole role);
    Task<CommitteeMember> AddMemberAsync(CommitteeMember member);

    Task UpdateCommitteeAsync(ConferenceCommittee committee);
    Task UpdateRoleAsync(CommitteeRole role);
    Task UpdateMemberAsync(CommitteeMember member);

    Task DeleteCommitteeAsync(ConferenceCommittee committee);
    Task DeleteRoleAsync(CommitteeRole role);
    Task DeleteMemberAsync(CommitteeMember member);
}