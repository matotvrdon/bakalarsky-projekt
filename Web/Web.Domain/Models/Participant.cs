using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Participant
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Phone { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }
    
    public int? ConferenceEntryId { get; set; }
    public ConferenceEntry? ConferenceEntry { get; set; }
    
    public bool IsStudent { get; set; } = false;
    public bool IsPresenting { get; set; }  = false;
    
    public List<FileManager> FileManagers { get; set; } = [];
    public List<ParticipantStatusAssignment> StatusAssignments { get; set; } = [];
    
    public int? UserId { get; set; }
    public User? User { get; set; }
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
}
