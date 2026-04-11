namespace Web.Services.DTOs;

public class ProgramDayDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int Order { get; set; }
    public List<ProgramItemDto>? ProgramItems { get; set; }
}
