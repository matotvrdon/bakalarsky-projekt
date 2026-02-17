using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Participant
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public RegistrationType RegistrationType { get; set; }
    public ParticipantStatus Status { get; set; }
    public ParticipantInvoiceStatus InvoiceStatus { get; set; }
    public DateTime RegistrationDate { get; set; }
}