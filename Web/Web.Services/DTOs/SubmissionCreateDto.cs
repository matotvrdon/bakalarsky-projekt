namespace Web.Services.DTOs;

public class SubmissionCreateDto
{
    public int ConferenceId { get; set; }
    public int? ParticipantId { get; set; }
    public required string Title { get; set; }
    public required string Abstract { get; set; }
    public required string Authors { get; set; }
    public required string Email { get; set; }
    public required string Affiliation { get; set; }
    public required string Category { get; set; }
    public string? Keywords { get; set; }
    public bool WillPresent { get; set; }
    public string? PaperFileUrl { get; set; }
}
