using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IConferenceSettingsService
{
    Task<ConferenceSettingsDto?> CreateAsync(int conferenceId, ConferenceSettingsCreateDto dto);
    Task<ImportantDatesDto?> UpdateAsync(int conferenceId, int importantDateId, ImportantDatesUpdatedDateDto dto);
}
