using Web.Services.DTOs.Committees;

namespace Web.Services.Abstractions;

public interface ICommitteeService
{
    Task<ConferenceCommitteesDto?> GetByConferenceIdAsync(int conferenceId);
    Task<ConferenceCommitteesDto?> GetByActiveConferenceAsync();

    Task<CommitteeDto?> CreateCommitteeAsync(int conferenceId, CommitteeCreateDto dto);
    Task<CommitteeDto?> UpdateCommitteeAsync(int committeeId, CommitteeUpdateDto dto);
    Task<bool> DeleteCommitteeAsync(int committeeId);

    Task<CommitteeRoleDto?> CreateRoleAsync(int committeeId, CommitteeRoleCreateDto dto);
    Task<CommitteeRoleDto?> UpdateRoleAsync(int roleId, CommitteeRoleUpdateDto dto);
    Task<bool> DeleteRoleAsync(int roleId);

    Task<CommitteeMemberDto?> CreateMemberAsync(int roleId, CommitteeMemberCreateDto dto);
    Task<CommitteeMemberDto?> UpdateMemberAsync(int memberId, CommitteeMemberUpdateDto dto);
    Task<bool> DeleteMemberAsync(int memberId);
}