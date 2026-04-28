namespace Web.Services.DTOs;

public class SubmissionDto
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int ConferenceId { get; set; }
    public required string SubmissionIdentifier { get; set; }
    public required string Title { get; set; }
    public bool IsPresenting { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
