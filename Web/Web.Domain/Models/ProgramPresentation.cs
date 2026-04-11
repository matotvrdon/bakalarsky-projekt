namespace Web.Domain.Models;

public class ProgramPresentation
{
    public int Id { get; set; }
    public int ProgramSessionId { get; set; }
    public ProgramSession ProgramSession { get; set; } = null!;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Authors { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
}
    
