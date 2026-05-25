namespace Web.Services.DTOs.ParticipantStatus;

public class ParticipantStatusDto
{
    public int Id { get; set; }

    public int ConferenceSettingsId { get; set; }

    public required string Name { get; set; }

    public bool RequiresApproval { get; set; }

    public bool IsActive { get; set; }

    public int Order { get; set; }
}