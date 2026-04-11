using System;

namespace Web.Domain.Models;

public class ConferenceSettings
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public Conference Conference { get; set; } = null!;
    public List<ImportantDates>? ImportantDates { get; set; }
    public List<FoodOptions>? FoodOptions { get; set; }
    public List<BookingOptions>? BookingOptions { get; set; }
    public List<ProgramDay>? ProgramDays { get; set; }
    public List<ConferenceEntry>? ConferenceEntries { get; set; }
}
