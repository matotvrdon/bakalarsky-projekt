using Web.Domain.Enums;

namespace Web.Domain.Models;

public class ProgramItem
{
    public int Id { get; set; }
    public int ProgramDayId { get; set; }
    public ProgramDay ProgramDay { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Location { get; set; }
    public string? Speaker { get; set; }
    public string? Chair { get; set; }
    public ProgramItemType Type { get; set; }
    public int Order { get; set; }
    public List<ProgramSession> ProgramSessions { get; set; } = [];
}
