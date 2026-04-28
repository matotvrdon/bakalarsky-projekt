using System;
using Web.Domain.Enums;

namespace Web.Domain.Models;

public class ImportantDates
{
    public int Id { get; set; }
    public int ConferenceSettingsId { get; set; }
    public required ConferenceSettings ConferenceSettings { get; set; }
    public required string Label { get; set; }
    public DateOnly NormalDate { get; set; }
    public DateOnly? UpdatedDate { get; set; }
    public ImportantDatesStatus ImportantDatesStatus { get; set; } = ImportantDatesStatus.Normal;
}
