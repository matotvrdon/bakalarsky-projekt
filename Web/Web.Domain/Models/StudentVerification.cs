using Web.Domain.Enums;

namespace Web.Domain.Models;

public class StudentVerification
{
    public int Id { get; set; }
    public StudentStatus Status { get; set; }
    public string? FilePath { get; set; }
    public string? OriginalFileName { get; set; }
    public string? ContentType { get; set; }
    public DateTime? UploadedAt { get; set; }

    public int ParticipantId { get; set; }
    public required Participant Participant { get; set; }
}
