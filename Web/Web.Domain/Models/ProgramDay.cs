namespace Web.Domain.Models;

public class ProgramDay
{
    public int Id { get; set; }
    public int ConferenceSettingsId { get; set; }
    public ConferenceSettings ConferenceSettings { get; set; } = null!;
    public string Label { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int Order { get; set; }
    public List<ProgramItem> ProgramItems { get; set; } = [];
}
