using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IParticipantStatusRepository _participantStatusRepository;
    private readonly IMapper _mapper;

    public ParticipantService(
        IParticipantRepository participantRepository,
        IParticipantStatusRepository participantStatusRepository,
        IMapper mapper)
    {
        _participantRepository = participantRepository;
        _participantStatusRepository = participantStatusRepository;
        _mapper = mapper;
    }

    public async Task<ParticipantDto?> GetByUserIdAsync(int userId)
    {
        var participant = await _participantRepository.GetByUserIdAsync(userId);

        return participant == null
            ? null
            : _mapper.Map<ParticipantDto>(participant);
    }

    public async Task<List<ParticipantDto>> GetAllAsync()
    {
        var participants = await _participantRepository.GetAllAsync();

        return _mapper.Map<List<ParticipantDto>>(participants);
    }

    public async Task<ParticipantDto?> UpdateAsync(ParticipantDto dto)
    {
        var participant = await _participantRepository.GetByIdAsync(dto.Id);

        if (participant == null)
        {
            return null;
        }

        participant.FirstName = dto.FirstName.Trim();
        participant.LastName = dto.LastName.Trim();
        participant.Phone = NormalizeOptional(dto.Phone);
        participant.Affiliation = NormalizeOptional(dto.Affiliation);
        participant.Country = NormalizeOptional(dto.Country);
        participant.ConferenceEntryId = dto.ConferenceEntryId;
        participant.IsStudent = dto.IsStudent ?? false;
        participant.IsPresenting = dto.IsPresenting ?? false;

        await _participantRepository.UpdateAsync(participant);

        var updatedParticipant = await _participantRepository.GetByIdAsync(participant.Id);

        return updatedParticipant == null
            ? null
            : _mapper.Map<ParticipantDto>(updatedParticipant);
    }

    public async Task<ParticipantDto?> UpdateStatusAssignmentsAsync(
        int participantId,
        ParticipantStatusAssignmentsUpdateDto dto)
    {
        var participant = await _participantRepository.GetByIdAsync(participantId);

        if (participant == null)
        {
            return null;
        }

        var requestedStatusIds = dto.ParticipantStatusIds
            .Distinct()
            .ToList();

        var validStatuses = await _participantStatusRepository.GetActiveByIdsForConferenceAsync(
            participant.ConferenceId,
            requestedStatusIds
        );

        if (validStatuses.Count != requestedStatusIds.Count)
        {
            throw new InvalidOperationException("One or more selected participant statuses are invalid.");
        }

        var currentAssignments = participant.StatusAssignments.ToList();
        var currentStatusIds = currentAssignments
            .Select(assignment => assignment.ParticipantStatusId)
            .ToHashSet();

        var requestedStatusIdSet = requestedStatusIds.ToHashSet();

        var assignmentsToRemove = currentAssignments
            .Where(assignment => !requestedStatusIdSet.Contains(assignment.ParticipantStatusId))
            .ToList();

        foreach (var assignment in assignmentsToRemove)
        {
            if (assignment.FileManagerId.HasValue || assignment.ApprovalState == StatusApprovalState.Approved)
            {
                throw new InvalidOperationException(
                    $"Status '{assignment.ParticipantStatus.Name}' cannot be removed because it already has a file or approval state."
                );
            }

            participant.StatusAssignments.Remove(assignment);
        }

        foreach (var status in validStatuses)
        {
            if (currentStatusIds.Contains(status.Id))
            {
                continue;
            }

            participant.StatusAssignments.Add(new ParticipantStatusAssignment
            {
                ParticipantId = participant.Id,
                ParticipantStatusId = status.Id,
                ApprovalState = status.RequiresApproval
                    ? StatusApprovalState.MissingFile
                    : StatusApprovalState.NotRequired
            });
        }

        await _participantRepository.UpdateAsync(participant);

        var updatedParticipant = await _participantRepository.GetByIdAsync(participant.Id);

        return updatedParticipant == null
            ? null
            : _mapper.Map<ParticipantDto>(updatedParticipant);
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}