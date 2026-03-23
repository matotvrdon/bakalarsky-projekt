namespace Web.Domain.Models;

public class Submission
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public required Participant Participant { get; set; }
    public int ConferenceId { get; set; }
    public required Conference Conference { get; set; }
    public required string SubmissionIdentifier { get; set; }
    public required string Title { get; set; }
    public bool IsPresenting { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
