using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services;

public class SubmissionSettingsService : ISubmissionSettingsService
{
    private readonly ISubmissionSettingsRepository _submissionSettingsRepository;
    private readonly IConferenceRepository _conferenceRepository;
    private readonly IMapper _mapper;

    public SubmissionSettingsService(
        ISubmissionSettingsRepository submissionSettingsRepository,
        IConferenceRepository conferenceRepository,
        IMapper mapper
    )
    {
        _submissionSettingsRepository = submissionSettingsRepository;
        _conferenceRepository = conferenceRepository;
        _mapper = mapper;
    }

    public async Task<SubmissionSettingsDto?> GetByConferenceIdAsync(int conferenceId)
    {
        var submissionSettings =
            await _submissionSettingsRepository.GetByConferenceIdAsync(conferenceId);

        return submissionSettings == null
            ? null
            : _mapper.Map<SubmissionSettingsDto>(submissionSettings);
    }

    public async Task<SubmissionSettingsDto?> GetActiveAsync()
    {
        var submissionSettings =
            await _submissionSettingsRepository.GetByActiveConferenceAsync();

        return submissionSettings == null
            ? null
            : _mapper.Map<SubmissionSettingsDto>(submissionSettings);
    }

    public async Task<SubmissionSettingsDto> CreateOrUpdateAsync(
        int conferenceId,
        SubmissionSettingsUpdateDto dto
    )
    {
        var conference = await _conferenceRepository.GetByIdAsync(conferenceId);

        if (conference == null)
        {
            throw new InvalidOperationException("Conference does not exist.");
        }

        if (conference.Settings == null)
        {
            conference.Settings = new ConferenceSettings
            {
                ConferenceId = conference.Id,
            };

            await _conferenceRepository.UpdateAsync(conference);
        }

        var existing =
            await _submissionSettingsRepository.GetByConferenceSettingsIdAsync(
                conference.Settings.Id
            );

        if (existing == null)
        {
            var created = _mapper.Map<SubmissionSettings>(dto);
            created.ConferenceSettingsId = conference.Settings.Id;

            await _submissionSettingsRepository.AddAsync(created);

            return _mapper.Map<SubmissionSettingsDto>(created);
        }

        _mapper.Map(dto, existing);

        await _submissionSettingsRepository.UpdateAsync(existing);

        return _mapper.Map<SubmissionSettingsDto>(existing);
    }

    public async Task<bool> DeleteAsync(int conferenceId)
    {
        var existing =
            await _submissionSettingsRepository.GetByConferenceIdAsync(conferenceId);

        if (existing == null)
        {
            return false;
        }

        await _submissionSettingsRepository.DeleteAsync(existing);

        return true;
    }
}