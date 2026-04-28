namespace Web.Services.DTOs.Committees;

public class CommitteeUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
}