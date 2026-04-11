namespace Web.Services.DTOs;

public class ProgramSessionDto
{
    public int Id { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Chair { get; set; }
    public int Order { get; set; }
    public List<ProgramPresentationDto>? Presentations { get; set; }
}
