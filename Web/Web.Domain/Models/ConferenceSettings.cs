using System;

namespace Web.Domain.Models;

public class ConferenceSettings
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public Conference Conference { get; set; } = null!;
    public List<ImportantDates>? ImportantDates { get; set; }
}
