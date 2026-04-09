using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ConferenceSettingsService : IConferenceSettingsService
{
    private readonly IConferenceSettingsRepository _conferenceSettingsRepository;
    private readonly IMapper _mapper;

    public ConferenceSettingsService(IConferenceSettingsRepository conferenceSettingsRepository, IMapper mapper)
    {
        _conferenceSettingsRepository = conferenceSettingsRepository;
        _mapper = mapper;
    }

    public async Task<ConferenceSettingsDto?> CreateAsync(int conferenceId, ConferenceSettingsCreateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings
        {
            ConferenceId = conference.Id,
            Conference = conference,
            ImportantDates = new List<ImportantDates>()
        };
        conference.Settings.ImportantDates ??= new List<ImportantDates>();

        var mappedImportantDates = _mapper.Map<List<ImportantDates>>(dto.ImportantDates ?? new List<ImportantDatesUpdateDto>());

        foreach (var importantDate in mappedImportantDates)
        {
            importantDate.UpdatedDate = null;
            importantDate.ImportantDatesStatus = ImportantDatesStatus.Normal;
            importantDate.ConferenceSettings = conference.Settings;
            conference.Settings.ImportantDates.Add(importantDate);
        }

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return _mapper.Map<ConferenceSettingsDto>(conference.Settings);
    }

    public async Task<ImportantDatesDto?> UpdateAsync(int conferenceId, int importantDateId, ImportantDatesUpdatedDateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings();
        conference.Settings.ImportantDates ??= new List<ImportantDates>();

        var importantDate = conference.Settings.ImportantDates
            .FirstOrDefault(date => date.Id == importantDateId);
        if (importantDate == null)
            return null;

        if (!string.IsNullOrWhiteSpace(dto.Label))
        {
            importantDate.Label = dto.Label.Trim();
        }

        importantDate.UpdatedDate = dto.UpdatedDate;
        importantDate.ImportantDatesStatus = ResolveStatus(importantDate.NormalDate, importantDate.UpdatedDate);

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return _mapper.Map<ImportantDatesDto>(importantDate);
    }

    private static ImportantDatesStatus ResolveStatus(DateOnly normalDate, DateOnly? updatedDate)
    {
        if (!updatedDate.HasValue || updatedDate.Value == normalDate)
            return ImportantDatesStatus.Normal;

        return updatedDate.Value > normalDate
            ? ImportantDatesStatus.Extended
            : ImportantDatesStatus.Shortened;
        
    }
}
