using Web.Domain.Enums;

namespace Web.Domain.Models;

public class ScheduleItem
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
    public DateTime Date { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public required string Title { get; set; }
    public ScheduleItemType Type { get; set; }
    public string? Location { get; set; }
    public int? SpeakerId { get; set; }
    public Speaker? Speaker { get; set; }
    public string? SpeakerName { get; set; }
}