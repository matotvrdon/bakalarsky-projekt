namespace Web.Domain.Models;

public class ConferenceCommittee
{
    public int Id { get; set; }

    public int ConferenceSettingsId { get; set; }
    public ConferenceSettings ConferenceSettings { get; set; } = null!;

    public required string Name { get; set; }
    public string? Description { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<CommitteeRole> Roles { get; set; }
}