namespace Web.Services.DTOs;

public class ConferenceDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsActive { get; set; }
    public int ParticipantsCount { get; set; }
}
