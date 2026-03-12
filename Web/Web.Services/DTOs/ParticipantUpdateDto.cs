using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ParticipantUpdateDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Phone { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }
    public RegistrationType? RegistrationType { get; set; }
    public bool? IsStudent { get; set; }
    public bool? IsPresenting { get; set; }
    public int UserId { get; set; }
    public int ConferenceId { get; set; }
}
