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
    
    
    public RegistrationType? RegistrationType { get; set; }
    
    
    // public int StudentVerificationId { get; set; }
    // public StudentVerification? StudentVerification { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
}
