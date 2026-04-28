using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class RegistrationSimpleResponseDto
{
    public required string Email { get; set; }
    public required string Message { get; set; }
}
