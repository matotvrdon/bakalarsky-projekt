namespace Web.Services.DTOs;

public class RegistrationErrorResponseDto
{
    public required string Code { get; set; }
    public required string Message { get; set; }
    public string? Field { get; set; }
}
