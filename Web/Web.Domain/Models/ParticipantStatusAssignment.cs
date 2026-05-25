using Web.Domain.Enums;

namespace Web.Domain.Models;

public class ParticipantStatusAssignment
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public Participant Participant { get; set; } = null!;
    public int ParticipantStatusId { get; set; }
    public ParticipantStatus ParticipantStatus { get; set; } = null!;
    public StatusApprovalState ApprovalState { get; set; } = StatusApprovalState.NotRequired;
    public int? FileManagerId { get; set; }
    public FileManager? FileManager { get; set; }
}