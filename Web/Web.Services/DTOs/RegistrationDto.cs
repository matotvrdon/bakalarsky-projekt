using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class RegistrationDto
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int ConferenceId { get; set; }
    public RegistrationType ParticipationType { get; set; }
    public ParticipantStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
