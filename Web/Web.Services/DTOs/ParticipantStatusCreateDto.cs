namespace Web.Services.DTOs;

public class ParticipantStatusCreateDto
{
    public required string Name { get; set; }

    public bool RequiresApproval { get; set; }

    public int Order { get; set; }
}