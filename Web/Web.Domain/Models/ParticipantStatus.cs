namespace Web.Domain.Models;

public class ParticipantStatus
{
    public int Id { get; set; }
    public int ConferenceSettingsId { get; set; }
    public ConferenceSettings ConferenceSettings { get; set; } = null!;
    public required string Name { get; set; }
    public bool RequiresApproval { get; set; }
    public bool IsActive { get; set; } = true;
    public int Order { get; set; }
    public List<ParticipantStatusAssignment> Assignments { get; set; } = [];
}