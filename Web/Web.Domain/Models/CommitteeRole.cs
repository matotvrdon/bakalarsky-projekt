namespace Web.Domain.Models;

public class CommitteeRole
{
    public int Id { get; set; }

    public int ConferenceCommitteeId { get; set; }
    public ConferenceCommittee ConferenceCommittee { get; set; } = null!;

    public required string Name { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<CommitteeMember> Members { get; set; }
}