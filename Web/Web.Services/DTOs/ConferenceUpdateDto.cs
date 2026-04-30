using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ConferenceUpdateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsPublished { get; set; }
    public ConferenceStatus Status { get; set; }
}