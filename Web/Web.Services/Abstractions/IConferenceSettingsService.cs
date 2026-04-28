using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IConferenceSettingsService
{
    Task<ConferenceSettingsDto?> CreateAsync(int conferenceId, ConferenceSettingsCreateDto dto);
    Task<ImportantDatesDto?> UpdateAsync(int conferenceId, int importantDateId, ImportantDatesUpdatedDateDto dto);
    Task<bool> DeleteAsync(int conferenceId, int importantDateId);
    Task<ConferenceSettingsDto?> CreateConferenceEntriesAsync(int conferenceId, ConferenceEntryCreateRequestDto dto);
    Task<ConferenceEntryDto?> UpdateConferenceEntryAsync(int conferenceId, int conferenceEntryId, ConferenceEntryUpdateDto dto);
    Task<bool> DeleteConferenceEntryAsync(int conferenceId, int conferenceEntryId);
    Task<ConferenceSettingsDto?> CreateFoodOptionsAsync(int conferenceId, FoodOptionsCreateRequestDto dto);
    Task<FoodOptionsDto?> UpdateFoodOptionAsync(int conferenceId, int foodOptionId, FoodOptionsUpdateDto dto);
    Task<bool> DeleteFoodOptionAsync(int conferenceId, int foodOptionId);
    Task<ConferenceSettingsDto?> CreateBookingOptionsAsync(int conferenceId, BookingOptionsCreateRequestDto dto);
    Task<BookingOptionsDto?> UpdateBookingOptionAsync(int conferenceId, int bookingOptionId, BookingOptionsUpdateDto dto);
    Task<bool> DeleteBookingOptionAsync(int conferenceId, int bookingOptionId);
    Task<ConferenceSettingsDto?> ReplaceProgramAsync(int conferenceId, ProgramReplaceRequestDto dto);
}
