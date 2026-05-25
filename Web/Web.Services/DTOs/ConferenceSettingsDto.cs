using Web.Services.DTOs.ParticipantStatus;

namespace Web.Services.DTOs;

public class ConferenceSettingsDto
{
    public List<ConferenceEntryDto>? ConferenceEntries { get; set; }
    public List<ImportantDatesDto>? ImportantDates { get; set; }
    public List<FoodOptionsDto>? FoodOptions { get; set; }
    public List<BookingOptionsDto>? BookingOptions { get; set; }
    public List<ProgramDayDto>? ProgramDays { get; set; }
    public List<ParticipantStatusDto> ParticipantStatuses { get; set; }
}
