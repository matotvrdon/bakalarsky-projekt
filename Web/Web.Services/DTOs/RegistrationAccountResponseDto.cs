namespace Web.Services.DTOs;

public class RegistrationAccountResponseDto
{
    public int ParticipantId { get; set; }
    public required UserDto User { get; set; }
    public required string Message { get; set; }
}
