namespace Web.Services.DTOs;

public class RegistrationBasicResponseDto
{
    public int ParticipantId { get; set; }
    public required string Status { get; set; }
    public required string Message { get; set; }
}
