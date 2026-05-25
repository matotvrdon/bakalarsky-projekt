using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ParticipantStatusAssignmentDto
{
    public int Id { get; set; }

    public int ParticipantId { get; set; }

    public int ParticipantStatusId { get; set; }

    public required string StatusName { get; set; }

    public bool RequiresApproval { get; set; }

    public StatusApprovalState ApprovalState { get; set; }

    public int? FileManagerId { get; set; }

    public string? OriginalFileName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }
}