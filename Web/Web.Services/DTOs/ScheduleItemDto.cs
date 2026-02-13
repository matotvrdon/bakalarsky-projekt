using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ScheduleItemDto
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public DateTime Date { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public required string Title { get; set; }
    public ScheduleItemType Type { get; set; }
    public string? Location { get; set; }
    public int? SpeakerId { get; set; }
    public string? SpeakerName { get; set; }
}
