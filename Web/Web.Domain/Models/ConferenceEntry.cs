namespace Web.Domain.Models;

public class ConferenceEntry
{
    public int Id { get; set; }
    public int ConferenceSettingsId { get; set; }
    public ConferenceSettings ConferenceSettings { get; set; } = null!;
    public required string Name { get; set; }
    public float Price { get; set; }
}
