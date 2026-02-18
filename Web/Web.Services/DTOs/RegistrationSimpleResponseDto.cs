using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class RegistrationSimpleResponseDto
{
    public string Email { get; set; }
    public string Message { get; set; }
    public MessageStatus MessageStatus { get; set; }
}