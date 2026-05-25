using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Conference
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsPublished { get; set; }
    public ConferenceStatus Status { get; set; } = ConferenceStatus.Preparation;
    public ConferenceSettings? Settings { get; set; }
}