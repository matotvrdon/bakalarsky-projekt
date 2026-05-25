namespace Web.Services.DTOs;

public class RegistrationBasicRequestDto
{
    public int ConferenceId { get; set; }

    public int? ConferenceEntryId { get; set; }

    public List<int> ParticipantStatusIds { get; set; } = new();

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? Phone { get; set; }

    public string? Affiliation { get; set; }

    public string? Country { get; set; }

    public bool IsStudent { get; set; }

    public bool IsPresenting { get; set; }
}