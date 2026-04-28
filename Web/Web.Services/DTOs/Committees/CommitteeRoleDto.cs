namespace Web.Services.DTOs.Committees;

public class CommitteeRoleDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Order { get; set; }

    public List<CommitteeMemberDto> Members { get; set; } = [];
}