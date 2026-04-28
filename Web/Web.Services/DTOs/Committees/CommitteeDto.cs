namespace Web.Services.DTOs.Committees;

public class CommitteeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int Order { get; set; }

    public List<CommitteeRoleDto> Roles { get; set; } = [];
}