namespace Web.Services.DTOs;

public class ConferenceUpdateDto
{
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsActive { get; set; }
}
