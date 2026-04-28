namespace Web.Domain.Models;

public class ProgramSession
{
    public int Id { get; set; }
    public int ProgramItemId { get; set; }
    public ProgramItem ProgramItem { get; set; } = null!;
    public string SessionName { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Chair { get; set; }
    public int Order { get; set; }
    public List<ProgramPresentation> ProgramPresentations { get; set; } = [];
}
