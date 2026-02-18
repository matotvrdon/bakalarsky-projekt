namespace Web.Services.DTOs;

public class EmailSendRequestDto
{
    public required string To { get; set; }
    public required string Password { get; set; }
}
