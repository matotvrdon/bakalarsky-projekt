namespace Web.Services.DTOs;

public class SubmissionCreateDto
{
    public int ParticipantId { get; set; }
    public int ConferenceId { get; set; }
    public required string SubmissionIdentifier { get; set; }
    public required string Title { get; set; }
    public bool IsPresenting { get; set; }
}
