namespace Web.Services.DTOs;

public class ConferenceCreateDto
{
    public required string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Location { get; set; }
}
