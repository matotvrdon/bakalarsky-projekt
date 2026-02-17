using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ParticipantUpdateDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public RegistrationType RegistrationType { get; set; }
    public ParticipantStatus Status { get; set; }
}
