namespace Web.Domain.Models;

public class InvitationEmail
{
    public int Id { get; set; }
    public required string Recipients { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public DateTime SentAt { get; set; }
    public string? Status { get; set; }
}