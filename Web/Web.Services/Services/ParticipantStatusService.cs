using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;
using Web.Services.DTOs.ParticipantStatus;

namespace Web.Services.Services;

public class ParticipantStatusService : IParticipantStatusService
{
    private readonly IParticipantStatusRepository _participantStatusRepository;
    private readonly IMapper _mapper;

    public ParticipantStatusService(
        IParticipantStatusRepository participantStatusRepository,
        IMapper mapper)
    {
        _participantStatusRepository = participantStatusRepository;
        _mapper = mapper;
    }

    public async Task<List<ParticipantStatusDto>> GetByConferenceIdAsync(int conferenceId)
    {
        var statuses = await _participantStatusRepository.GetByConferenceIdAsync(conferenceId);

        return _mapper.Map<List<ParticipantStatusDto>>(statuses);
    }

    public async Task<ParticipantStatusDto?> CreateAsync(
        int conferenceId,
        ParticipantStatusCreateDto dto
    )
    {
        var settings = await _participantStatusRepository
            .GetConferenceSettingsByConferenceIdAsync(conferenceId);

        if (settings == null)
        {
            return null;
        }

        var name = dto.Name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var exists = await _participantStatusRepository.ExistsByNameAsync(
            settings.Id,
            name
        );

        if (exists)
        {
            throw new InvalidOperationException("Status s rovnakým názvom už existuje.");
        }

        var status = new ParticipantStatus
        {
            ConferenceSettingsId = settings.Id,
            Name = name,
            RequiresApproval = dto.RequiresApproval,
            IsActive = true,
            Order = dto.Order
        };

        await _participantStatusRepository.AddAsync(status);

        return _mapper.Map<ParticipantStatusDto>(status);
    }

    public async Task<ParticipantStatusDto?> UpdateAsync(
        int conferenceId,
        int statusId,
        ParticipantStatusUpdateDto dto
    )
    {
        var status = await _participantStatusRepository.GetByIdAsync(statusId);

        if (status == null)
        {
            return null;
        }

        if (status.ConferenceSettings.ConferenceId != conferenceId)
        {
            return null;
        }

        var name = dto.Name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var exists = await _participantStatusRepository.ExistsByNameAsync(
            status.ConferenceSettingsId,
            name,
            status.Id
        );

        if (exists)
        {
            throw new InvalidOperationException("Status s rovnakým názvom už existuje.");
        }

        status.Name = name;
        status.RequiresApproval = dto.RequiresApproval;
        status.IsActive = dto.IsActive;
        status.Order = dto.Order;

        await _participantStatusRepository.UpdateAsync(status);

        return _mapper.Map<ParticipantStatusDto>(status);
    }

    public async Task<bool> DeactivateAsync(int conferenceId, int statusId)
    {
        var status = await _participantStatusRepository.GetByIdAsync(statusId);

        if (status == null)
        {
            return false;
        }

        if (status.ConferenceSettings.ConferenceId != conferenceId)
        {
            return false;
        }

        status.IsActive = false;

        await _participantStatusRepository.UpdateAsync(status);

        return true;
    }
}