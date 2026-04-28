namespace Web.Domain.Models;

public class CommitteeMember
{
    public int Id { get; set; }

    public int CommitteeRoleId { get; set; }
    public CommitteeRole CommitteeRole { get; set; } = null!;

    public required string FullName { get; set; }

    public string? Position { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}