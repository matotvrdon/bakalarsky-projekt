using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required UserRole Role { get; set; }
    public string? Name { get; set; }
}
