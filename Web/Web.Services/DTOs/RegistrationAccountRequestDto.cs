namespace Web.Services.DTOs;

public class RegistrationAccountRequestDto
{
    public int ParticipantId { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}
