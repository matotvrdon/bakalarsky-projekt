namespace Web.Services.DTOs;

public class ParticipantStatusUpdateDto
{
    public required string Name { get; set; }

    public bool RequiresApproval { get; set; }

    public bool IsActive { get; set; }

    public int Order { get; set; }
}