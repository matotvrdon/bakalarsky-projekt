using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Committees;

namespace Web.Services;

public class CommitteeService : ICommitteeService
{
    private readonly ICommitteeRepository _committeeRepository;
    private readonly IMapper _mapper;

    public CommitteeService(
        ICommitteeRepository committeeRepository,
        IMapper mapper)
    {
        _committeeRepository = committeeRepository;
        _mapper = mapper;
    }

    public async Task<ConferenceCommitteesDto?> GetByConferenceIdAsync(int conferenceId)
    {
        List<ConferenceCommittee> committees = await _committeeRepository.GetByConferenceIdAsync(conferenceId);

        if (committees.Count == 0)
        {
            return null;
        }

        return MapToConferenceCommitteesDto(committees);
    }

    public async Task<ConferenceCommitteesDto?> GetByActiveConferenceAsync()
    {
        List<ConferenceCommittee> committees = await _committeeRepository.GetByActiveConferenceAsync();

        if (committees.Count == 0)
        {
            return null;
        }

        return MapToConferenceCommitteesDto(committees);
    }

    public async Task<CommitteeDto?> CreateCommitteeAsync(int conferenceId, CommitteeCreateDto dto)
    {
        ConferenceSettings? settings = await _committeeRepository.GetSettingsByConferenceIdAsync(conferenceId);

        if (settings == null || string.IsNullOrWhiteSpace(dto.Name))
        {
            return null;
        }

        ConferenceCommittee committee = _mapper.Map<ConferenceCommittee>(dto);
        committee.Name = dto.Name.Trim();
        committee.Description = string.IsNullOrWhiteSpace(dto.Description)
            ? null
            : dto.Description.Trim();
        committee.ConferenceSettingsId = settings.Id;
        committee.CreatedAt = DateTime.UtcNow;

        ConferenceCommittee createdCommittee = await _committeeRepository.AddCommitteeAsync(committee);

        return _mapper.Map<CommitteeDto>(createdCommittee);
    }

    public async Task<CommitteeDto?> UpdateCommitteeAsync(int committeeId, CommitteeUpdateDto dto)
    {
        ConferenceCommittee? committee = await _committeeRepository.GetCommitteeByIdAsync(committeeId);

        if (committee == null || string.IsNullOrWhiteSpace(dto.Name))
        {
            return null;
        }

        _mapper.Map(dto, committee);

        committee.Name = dto.Name.Trim();
        committee.Description = string.IsNullOrWhiteSpace(dto.Description)
            ? null
            : dto.Description.Trim();
        committee.UpdatedAt = DateTime.UtcNow;

        await _committeeRepository.UpdateCommitteeAsync(committee);

        return _mapper.Map<CommitteeDto>(committee);
    }

    public async Task<bool> DeleteCommitteeAsync(int committeeId)
    {
        ConferenceCommittee? committee = await _committeeRepository.GetCommitteeByIdAsync(committeeId);

        if (committee == null)
        {
            return false;
        }

        await _committeeRepository.DeleteCommitteeAsync(committee);

        return true;
    }

    public async Task<CommitteeRoleDto?> CreateRoleAsync(int committeeId, CommitteeRoleCreateDto dto)
    {
        ConferenceCommittee? committee = await _committeeRepository.GetCommitteeByIdAsync(committeeId);

        if (committee == null || string.IsNullOrWhiteSpace(dto.Name))
        {
            return null;
        }

        CommitteeRole role = _mapper.Map<CommitteeRole>(dto);
        role.Name = dto.Name.Trim();
        role.ConferenceCommitteeId = committee.Id;
        role.CreatedAt = DateTime.UtcNow;

        CommitteeRole createdRole = await _committeeRepository.AddRoleAsync(role);

        return _mapper.Map<CommitteeRoleDto>(createdRole);
    }

    public async Task<CommitteeRoleDto?> UpdateRoleAsync(int roleId, CommitteeRoleUpdateDto dto)
    {
        CommitteeRole? role = await _committeeRepository.GetRoleByIdAsync(roleId);

        if (role == null || string.IsNullOrWhiteSpace(dto.Name))
        {
            return null;
        }

        _mapper.Map(dto, role);

        role.Name = dto.Name.Trim();
        role.UpdatedAt = DateTime.UtcNow;

        await _committeeRepository.UpdateRoleAsync(role);

        return _mapper.Map<CommitteeRoleDto>(role);
    }

    public async Task<bool> DeleteRoleAsync(int roleId)
    {
        CommitteeRole? role = await _committeeRepository.GetRoleByIdAsync(roleId);

        if (role == null)
        {
            return false;
        }

        await _committeeRepository.DeleteRoleAsync(role);

        return true;
    }

    public async Task<CommitteeMemberDto?> CreateMemberAsync(int roleId, CommitteeMemberCreateDto dto)
    {
        CommitteeRole? role = await _committeeRepository.GetRoleByIdAsync(roleId);

        if (role == null || string.IsNullOrWhiteSpace(dto.FullName))
        {
            return null;
        }

        CommitteeMember member = _mapper.Map<CommitteeMember>(dto);
        member.FullName = dto.FullName.Trim();
        member.Position = string.IsNullOrWhiteSpace(dto.Position) ? null : dto.Position.Trim();
        member.Affiliation = string.IsNullOrWhiteSpace(dto.Affiliation) ? null : dto.Affiliation.Trim();
        member.Country = string.IsNullOrWhiteSpace(dto.Country) ? null : dto.Country.Trim();
        member.CommitteeRoleId = role.Id;
        member.CreatedAt = DateTime.UtcNow;

        CommitteeMember createdMember = await _committeeRepository.AddMemberAsync(member);

        return _mapper.Map<CommitteeMemberDto>(createdMember);
    }

    public async Task<CommitteeMemberDto?> UpdateMemberAsync(int memberId, CommitteeMemberUpdateDto dto)
    {
        CommitteeMember? member = await _committeeRepository.GetMemberByIdAsync(memberId);

        if (member == null || string.IsNullOrWhiteSpace(dto.FullName))
        {
            return null;
        }

        _mapper.Map(dto, member);

        member.FullName = dto.FullName.Trim();
        member.Position = string.IsNullOrWhiteSpace(dto.Position) ? null : dto.Position.Trim();
        member.Affiliation = string.IsNullOrWhiteSpace(dto.Affiliation) ? null : dto.Affiliation.Trim();
        member.Country = string.IsNullOrWhiteSpace(dto.Country) ? null : dto.Country.Trim();
        member.UpdatedAt = DateTime.UtcNow;

        await _committeeRepository.UpdateMemberAsync(member);

        return _mapper.Map<CommitteeMemberDto>(member);
    }

    public async Task<bool> DeleteMemberAsync(int memberId)
    {
        CommitteeMember? member = await _committeeRepository.GetMemberByIdAsync(memberId);

        if (member == null)
        {
            return false;
        }

        await _committeeRepository.DeleteMemberAsync(member);

        return true;
    }

    private ConferenceCommitteesDto MapToConferenceCommitteesDto(List<ConferenceCommittee> committees)
    {
        ConferenceCommittee firstCommittee = committees[0];

        ConferenceCommitteesDto dto = new ConferenceCommitteesDto
        {
            ConferenceId = firstCommittee.ConferenceSettings.ConferenceId,
            ConferenceName = firstCommittee.ConferenceSettings.Conference.Name,
            Committees = _mapper.Map<List<CommitteeDto>>(
                committees.OrderBy(committee => committee.Order).ToList()
            )
        };

        return dto;
    }
}