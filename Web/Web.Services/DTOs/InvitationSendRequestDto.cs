namespace Web.Services.DTOs;

public class InvitationSendRequestDto
{
    public List<string> Emails { get; set; } = new();
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
