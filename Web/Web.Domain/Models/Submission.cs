using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Submission
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
    public int? ParticipantId { get; set; }
    public Participant? Participant { get; set; }
    public required string Title { get; set; }
    public required string Abstract { get; set; }
    public required string Authors { get; set; }
    public required string Email { get; set; }
    public required string Affiliation { get; set; }
    public required string Category { get; set; }
    public string? Keywords { get; set; }
    public SubmissionStatus Status { get; set; }
    public bool WillPresent { get; set; }
    public string? PaperFileUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}