using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Registration
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public Participant? Participant { get; set; }
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
    public RegistrationType ParticipationType { get; set; }
    public ParticipantStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}