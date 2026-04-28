namespace Web.Services.DTOs.Committees;

public class CommitteeCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
}